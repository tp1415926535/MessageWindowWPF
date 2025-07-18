using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace MessageWindowWPF
{
    public partial class Prompt
    {

        public static Prompt Show(string content, double liveSeconds = 3, Window owner = null, Point? point = null, Color? backColor = null)
        {
            return ShowPrompt(content, null, liveSeconds, owner, point, backColor);
        }

        public static Prompt Show(IEnumerable<Inline> inlines, double liveSeconds = 3, Window owner = null, Point? point = null, Color? backColor = null)
        {
            return ShowPrompt(null, inlines, liveSeconds, owner, point, backColor);
        }

        public static Prompt Show(PromptParams promptParams)
        {
            return ShowPrompt(promptParams);
        }
    }
}
