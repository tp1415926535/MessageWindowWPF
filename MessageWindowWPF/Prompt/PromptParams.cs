using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace MessageWindowWPF
{
    public class PromptParams
    {
        public PromptParams() { }

        public PromptParams(string content = null, IEnumerable<Inline> inlines = null, double liveSeconds = 3, Window owner = null, Point? point = null, Color? backColor = null)
        {
            Content = content;
            Inlines = inlines;
            LiveSeconds = liveSeconds;
            Owner = owner;
            Location = point;
            BackColor = backColor;
        }

        public string Content { get; set; }

        public IEnumerable<Inline> Inlines { get; set; }

        public double LiveSeconds { get; set; } = 3;

        public Window Owner { get; set; }

        public Point? Location { get; set; }

        public Color? BackColor { get; set; }

        public Color? ForeColor { get; set; }

        /// <summary>
        /// 0~1
        /// </summary>
        public double? BackOpacity { get; set; }

        public Action Action { get; set; }

        public string ActionText { get; set; }

        public Color? ActionForeColor { get; set; }
    }
}
