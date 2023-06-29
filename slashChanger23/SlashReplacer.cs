using System.Text;
using System.Windows;

namespace slashChanger23;

public static class SlashReplacer
{
    /// <summary>
    /// Заменяет слеши в тексте из буфера обмена на противоположные и добавляет текст в буфер обмена.
    /// </summary>
    /// <param name="textData">Исходный текст.</param>
    public static void ReplaceText()
    {
        string? textData = null;
        
        if(Clipboard.ContainsText()) 
            textData = Clipboard.GetText();

        if(textData!=null)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var str in textData)
            {
                if (str == '/')
                    stringBuilder.Append('\\');
                else if (str == '\\')
                    stringBuilder.Append('/');
                else
                {
                    stringBuilder.Append(str);
                }
            }

            Clipboard.SetText(stringBuilder.ToString());
        }
    }
}