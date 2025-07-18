using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace MessageWindowWPF
{
    public class MessageBoxParams
    {
        public MessageBoxParams() { }

        public MessageBoxParams(string messageBoxText = null, 
            string caption = null, 
            MessageBoxButton button = MessageBoxButton.OK, 
            MessageBoxImage icon = MessageBoxImage.None, 
            Window owner = null, 
            MessageBoxResult defaultResult = MessageBoxResult.None, 
            IEnumerable<Inline> inlines = null, 
            bool playSound = false)
        {
            Text = messageBoxText;
            Buttons = button;
            Icon = icon;
            Owner = owner;
            Title = caption;
            DefaultResult = defaultResult;
            Inlines = inlines;
            PlaySound = playSound;
        }

        public string Text { get; set; }

        public string Title { get; set; }

        public MessageBoxButton Buttons { get; set; } = MessageBoxButton.OK;

        public MessageBoxImage Icon { get; set; } = MessageBoxImage.None;

        public Window Owner { get; set; }

        public MessageBoxResult DefaultResult { get; set; } = MessageBoxResult.None;

        public IEnumerable<Inline> Inlines { get; set; }

        public bool PlaySound { get; set; }

        #region custom button text
        public string CustomOkText { get; set; }

        public string CustomCancelText { get; set; }

        public string CustomYesText { get; set; }

        public string CustomNoText { get; set; }
        #endregion
    }
}
