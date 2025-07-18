using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace MessageWindowWPF
{
    public partial class MessageBox
    {
        public static MessageBoxResult Show(string messageBoxText = null, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, Window owner = null, string caption = null, MessageBoxResult defaultResult = MessageBoxResult.None, IEnumerable<Inline> inlines = null, bool playSound = false)
        {
            return ShowMessageBox(messageBoxText, button, icon, owner, caption, defaultResult, inlines, playSound);
        }

        public static MessageBoxResult Show(string messageBoxText)
        {
            return ShowMessageBox(messageBoxText);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            return ShowMessageBox(messageBoxText, MessageBoxButton.OK, MessageBoxImage.None, null, caption);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return ShowMessageBox(messageBoxText, button, MessageBoxImage.None, null, caption);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return ShowMessageBox(messageBoxText, button, icon, null, caption);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return ShowMessageBox(messageBoxText, button, icon, null, caption, defaultResult);
        }

        public static MessageBoxResult Show(IEnumerable<Inline> inlines)
        {
            return ShowMessageBox(null, MessageBoxButton.OK, MessageBoxImage.None, null, null, MessageBoxResult.None, inlines);
        }

        public static MessageBoxResult Show(IEnumerable<Inline> inlines, string caption)
        {
            return ShowMessageBox(null, MessageBoxButton.OK, MessageBoxImage.None, null, caption, MessageBoxResult.None, inlines);
        }

        public static MessageBoxResult Show(IEnumerable<Inline> inlines, string caption, MessageBoxButton button)
        {
            return ShowMessageBox(null, button, MessageBoxImage.None, null, caption, MessageBoxResult.None, inlines);
        }

        public static MessageBoxResult Show(IEnumerable<Inline> inlines, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return ShowMessageBox(null, button, icon, null, caption, MessageBoxResult.None, inlines);
        }

        public static MessageBoxResult Show(IEnumerable<Inline> inlines, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return ShowMessageBox(null, button, icon, null, caption, defaultResult, inlines);
        }


        public static MessageBoxResult Show(Window owner, string messageBoxText)
        {
            return ShowMessageBox(messageBoxText, MessageBoxButton.OK, MessageBoxImage.None, owner);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption)
        {
            return ShowMessageBox(messageBoxText, MessageBoxButton.OK, MessageBoxImage.None, owner, caption);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            return ShowMessageBox(messageBoxText, button, MessageBoxImage.None, owner, caption);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return ShowMessageBox(messageBoxText, button, icon, owner, caption);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return ShowMessageBox(messageBoxText, button, icon, owner, caption, defaultResult);
        }


        public static MessageBoxResult Show(Window owner, IEnumerable<Inline> inlines)
        {
            return ShowMessageBox(null, MessageBoxButton.OK, MessageBoxImage.None, owner, null, MessageBoxResult.None, inlines);
        }

        public static MessageBoxResult Show(Window owner, IEnumerable<Inline> inlines, string caption)
        {
            return ShowMessageBox(null, MessageBoxButton.OK, MessageBoxImage.None, owner, caption, MessageBoxResult.None, inlines);
        }

        public static MessageBoxResult Show(Window owner, IEnumerable<Inline> inlines, string caption, MessageBoxButton button)
        {
            return ShowMessageBox(null, button, MessageBoxImage.None, owner, caption, MessageBoxResult.None, inlines);
        }

        public static MessageBoxResult Show(Window owner, IEnumerable<Inline> inlines, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return ShowMessageBox(null, button, icon, owner, caption, MessageBoxResult.None, inlines);
        }

        public static MessageBoxResult Show(Window owner, IEnumerable<Inline> inlines, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return ShowMessageBox(null, button, icon, owner, caption, defaultResult, inlines);
        }

        public static MessageBoxResult Show(MessageBoxParams messageBoxParams)
        {
            return ShowMessageBox(messageBoxParams);
        }
    }
}
