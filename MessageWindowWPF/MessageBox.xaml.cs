﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MessageWindowWPF
{
    /// <summary>
    /// MessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBox : Window
    {
        private static MessageBox customMessageBox;
        private MessageBoxResult result;
        public MessageBox()
        {
            InitializeComponent();
        }

        #region innerMethod
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.OK;
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.Cancel;
            this.Close();
        }
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.Yes;
            this.Close();
        }
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.No;
            this.Close();
        }
        public void YesCommand(Object sender, ExecutedRoutedEventArgs e)
        {
            if (YesNoButtonsGrid.Visibility == Visibility.Visible || YesNoCancelButtonsGrid.Visibility == Visibility.Visible)
            {
                result = MessageBoxResult.Yes;
                this.Close();
            }
        }
        public void NoCommand(Object sender, ExecutedRoutedEventArgs e)
        {
            if (YesNoButtonsGrid.Visibility == Visibility.Visible || YesNoCancelButtonsGrid.Visibility == Visibility.Visible)
            {
                result = MessageBoxResult.No;
                this.Close();
            }
        }
        public void CopyCommand(Object sender, ExecutedRoutedEventArgs e)
        {
            Clipboard.SetText(MessageText.Text);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (result == MessageBoxResult.None)//没选直接关闭
            {
                if (OKButtonsGrid.Visibility == Visibility.Visible)
                    result = MessageBoxResult.OK;
                else if (OKCancelButtonsGrid.Visibility == Visibility.Visible)
                    result = MessageBoxResult.Cancel;
                else if (YesNoCancelButtonsGrid.Visibility == Visibility.Visible)
                    result = MessageBoxResult.Cancel;
                else if (YesNoButtonsGrid.Visibility == Visibility.Visible)
                    e.Cancel = true;
            }
        }




        private void SetWindowStyle()
        {
            if (MessageSetting.NoSystemHeader)
            {
                this.WindowStyle = WindowStyle.None;
                this.AllowsTransparency = true;
                MainBorder.Margin = new Thickness(12);
                MainBorder.BorderThickness = new Thickness(1);
                MainBorder.Effect = (System.Windows.Media.Effects.Effect)Resources["shadowEffect"];
                if (MessageSetting.WithCornerRadius)
                    MainBorder.CornerRadius = new CornerRadius(5);
            }
        }

        private void SetButtonVisible(MessageBoxButton button, MessageBoxResult defaultResult)
        {
            switch (button)
            {
                case MessageBoxButton.OK:
                    OKButtonsGrid.Visibility = Visibility.Visible;
                    if (defaultResult != MessageBoxResult.None)
                    {
                        OKButtonsGrid.Children[0].Focus();
                        result = MessageBoxResult.OK;
                    }
                    break;
                case MessageBoxButton.OKCancel:
                    OKCancelButtonsGrid.Visibility = Visibility.Visible;
                    switch (defaultResult)
                    {
                        case MessageBoxResult.OK:
                            OKCancelButtonsGrid.Children[0].Focus();
                            result = MessageBoxResult.OK;
                            break;
                        case MessageBoxResult.Cancel:
                            OKCancelButtonsGrid.Children[1].Focus();
                            result = MessageBoxResult.Cancel;
                            break;
                        case MessageBoxResult.None:
                            //OKCancelButtonsGrid.Children[0].Focus();
                            break;
                    }
                    break;
                case MessageBoxButton.YesNo:
                    this.DisableCloseButton();
                    CloseButton.IsEnabled = false;
                    YesNoButtonsGrid.Visibility = Visibility.Visible;
                    switch (defaultResult)
                    {
                        case MessageBoxResult.Yes:
                            YesNoButtonsGrid.Children[0].Focus();
                            result = MessageBoxResult.Yes;
                            break;
                        case MessageBoxResult.No:
                            YesNoButtonsGrid.Children[1].Focus();
                            result = MessageBoxResult.No;
                            break;
                        case MessageBoxResult.None:
                            //YesNoButtonsGrid.Children[0].Focus();
                            break;
                    }
                    break;
                case MessageBoxButton.YesNoCancel:
                    YesNoCancelButtonsGrid.Visibility = Visibility.Visible;
                    switch (defaultResult)
                    {
                        case MessageBoxResult.Yes:
                            YesNoCancelButtonsGrid.Children[0].Focus();
                            result = MessageBoxResult.Yes;
                            break;
                        case MessageBoxResult.No:
                            YesNoCancelButtonsGrid.Children[1].Focus();
                            result = MessageBoxResult.No;
                            break;
                        case MessageBoxResult.Cancel:
                            YesNoCancelButtonsGrid.Children[2].Focus();
                            result = MessageBoxResult.Cancel;
                            break;
                        case MessageBoxResult.None:
                            //YesNoCancelButtonsGrid.Children[0].Focus();
                            break;

                    }
                    break;
            }
        }
        private static BitmapSource GetIcon(MessageBoxImage messageBoxImage)
        {
            switch (messageBoxImage)
            {
                case MessageBoxImage.Error:
                    return new BitmapImage(new Uri("/MessageWindowWPF;component/Icons/cancel.png", UriKind.Relative));
                case MessageBoxImage.Warning:
                    return new BitmapImage(new Uri("/MessageWindowWPF;component/Icons/error.png", UriKind.Relative));
                case MessageBoxImage.Information:
                    return new BitmapImage(new Uri("/MessageWindowWPF;component/Icons/info.png", UriKind.Relative));
                case MessageBoxImage.Question:
                    return new BitmapImage(new Uri("/MessageWindowWPF;component/Icons/help.png", UriKind.Relative));
                default:
                    return null;
            }
            //System.Drawing.Icon icon = (System.Drawing.Icon)typeof(System.Drawing.SystemIcons).
            //GetProperty(messageBoxImage.ToString(), BindingFlags.Public | BindingFlags.Static).GetValue(null, null);
            //return bs = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private void AdjustMaxWidth(int textLength)
        {
            if (textLength > 1900)
            {
                double nWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double nHieght = System.Windows.SystemParameters.PrimaryScreenHeight;
                double rate = nWidth / nHieght;

                double maxW = 1200;
                var h = textLength * MessageText.FontSize / maxW * MessageText.FontSize * 1.7 + 150;
                while (maxW / h < rate)
                {
                    if (maxW + 100 >= nWidth)
                        break;
                    maxW += 100;
                    h = textLength * MessageText.FontSize / maxW * MessageText.FontSize * 1.7 + 150;
                }
                this.MaxWidth = maxW;
            }
        }
        private static void CalculateInlineLength(IEnumerable<Inline> inlines, ref int length)
        {
            foreach (var inline in inlines)
            {
                if (inline is Bold)
                    CalculateInlineLength(((Bold)inline).Inlines, ref length);
                else if (inline is Italic)
                    CalculateInlineLength(((Italic)inline).Inlines, ref length);
                else if (inline is Underline)
                    CalculateInlineLength(((Underline)inline).Inlines, ref length);
                else if (inline is Hyperlink)
                    CalculateInlineLength(((Hyperlink)inline).Inlines, ref length);
                else
                {
                    Run run = inline as Run;
                    if (run == null) continue;
                    length += run.Text.Length;
                }
            }
        }
        #endregion




        private static MessageBoxResult ShowMessageBox(string messageBoxText = null, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, Window owner = null, string caption = null, MessageBoxResult defaultResult = MessageBoxResult.None, IEnumerable<Inline> inlines = null, bool playSound = false)
        {
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                customMessageBox = new MessageBox();

                customMessageBox.SetWindowStyle();

                if (!string.IsNullOrEmpty(messageBoxText))
                    customMessageBox.MessageText.Text = messageBoxText;
                if (inlines != null)
                    customMessageBox.MessageText.Inlines.AddRange(inlines);

                customMessageBox.SetButtonVisible(button, defaultResult);

                customMessageBox.MessageIcon.Source = GetIcon(icon);

                try
                {
                    if (owner != null)
                        customMessageBox.Owner = owner;
                    else
                        customMessageBox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
                catch
                {
                    customMessageBox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }

                if (!string.IsNullOrEmpty(caption))
                    customMessageBox.Title = caption;

                if (!string.IsNullOrEmpty(messageBoxText))
                    customMessageBox.AdjustMaxWidth(messageBoxText.Length);
                else if (inlines != null)
                {
                    int textLength = 0;
                    CalculateInlineLength(inlines, ref textLength);
                    customMessageBox.AdjustMaxWidth(textLength);
                }

                if (playSound)
                    System.Media.SystemSounds.Beep.Play();//new System.Media.SoundPlayer(@"C:\Windows\Media\Windows Ding.wav").Play();

                customMessageBox.ShowDialog();
            }));
            if (customMessageBox == null) return MessageBoxResult.None;
            var messageResult = customMessageBox.result;
            customMessageBox = null;
            return messageResult;
        }



        #region outMethods
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

        #endregion
    }

    public static class WinApi
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        const uint MF_BYCOMMAND = 0x00000000;
        const uint MF_GRAYED = 0x00000001;

        const uint SC_CLOSE = 0xF060;

        public static void DisableCloseButton(this System.Windows.Window window)
        {
            // Disable close button
            IntPtr hwnd = new WindowInteropHelper(window).EnsureHandle();
            IntPtr hMenu = GetSystemMenu(hwnd, false);

            if (hMenu != IntPtr.Zero)
                EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
        }
    }
}
