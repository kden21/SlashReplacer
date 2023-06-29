using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;

namespace slashChanger23
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Добавляет хоткей.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        
        /// <summary>
        /// Удаляет хоткей.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        
        /// <summary>
        /// Идентификатор действия хоткея.
        /// </summary>
        const int ActionHotKeyId = 1;
        
        [Flags]
        public enum Modifiers
        {
            Ctrl = 0x0002
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            
            var source = PresentationSource.FromVisual(this as Visual) as HwndSource;
            if (source == null)
                throw new Exception("Could not create hWnd source from window.");
            source.AddHook(WndProc);

            RegisterHotKey(new WindowInteropHelper(this).Handle, ActionHotKeyId, (int)Modifiers.Ctrl, (int)Keys.B);
            Visibility=Visibility.Hidden;
        }
        
        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0x0312)
            {
                SlashReplacer.ReplaceText();
            }

            return IntPtr.Zero;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            UnregisterHotKey(new WindowInteropHelper(this).Handle, ActionHotKeyId);
            this.Close();
        }
    }
}