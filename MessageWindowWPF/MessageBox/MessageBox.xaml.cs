using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace MessageWindowWPF
{
    /// <summary>
    /// MessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBox : Window
    {
        private static MessageBox messageBox;
        private MessageBoxResult result;
        public MessageBox()
        {
            InitializeComponent();
        }

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
            if (result == MessageBoxResult.None)//set as default result
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

        private void SetCustomText(MessageBoxParams inputParam)
        {
            switch (inputParam.Buttons)
            {
                case MessageBoxButton.OK:
                    if (!string.IsNullOrEmpty(inputParam.CustomOkText))
                        messageBox.OkGridOkBtn.Content = inputParam.CustomOkText;
                    break;
                case MessageBoxButton.OKCancel:
                    if (!string.IsNullOrEmpty(inputParam.CustomOkText))
                        messageBox.OCGridOkBtn.Content = inputParam.CustomOkText;
                    if (!string.IsNullOrEmpty(inputParam.CustomCancelText))
                        messageBox.OCGridCancelBtn.Content = inputParam.CustomCancelText;
                    break;
                case MessageBoxButton.YesNo:
                    if (!string.IsNullOrEmpty(inputParam.CustomYesText))
                        messageBox.YNGridYesBtn.Content = inputParam.CustomYesText;
                    if (!string.IsNullOrEmpty(inputParam.CustomNoText))
                        messageBox.YNGridNoBtn.Content = inputParam.CustomNoText;
                    break;
                case MessageBoxButton.YesNoCancel:
                    if (!string.IsNullOrEmpty(inputParam.CustomYesText))
                        messageBox.YNCGridYesBtn.Content = inputParam.CustomYesText;
                    if (!string.IsNullOrEmpty(inputParam.CustomNoText))
                        messageBox.YNCGridNoBtn.Content = inputParam.CustomNoText;
                    if (!string.IsNullOrEmpty(inputParam.CustomCancelText))
                        messageBox.YNCGridCancelBtn.Content = inputParam.CustomCancelText;
                    break;
            }
        }

        private static MessageBoxResult ShowMessageBox(string messageBoxText = null, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, Window owner = null, string caption = null, MessageBoxResult defaultResult = MessageBoxResult.None, IEnumerable<Inline> inlines = null, bool playSound = false)
        {
            return ShowMessageBox(new MessageBoxParams(messageBoxText, caption, button, icon, owner, defaultResult, inlines, playSound));
        }


        private static MessageBoxResult ShowMessageBox(MessageBoxParams inputParam)
        {
            inputParam = inputParam ?? new MessageBoxParams();
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                messageBox = new MessageBox();

                messageBox.SetWindowStyle();

                if (!string.IsNullOrEmpty(inputParam.Text))
                    messageBox.MessageText.Text = inputParam.Text;
                if (inputParam.Inlines != null)
                    messageBox.MessageText.Inlines.AddRange(inputParam.Inlines);

                messageBox.SetButtonVisible(inputParam.Buttons, inputParam.DefaultResult);

                messageBox.MessageIcon.Source = GetIcon(inputParam.Icon);

                try
                {
                    if (inputParam.Owner != null)
                        messageBox.Owner = inputParam.Owner;
                    else
                        messageBox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
                catch
                {
                    messageBox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }

                if (!string.IsNullOrEmpty(inputParam.Title))
                    messageBox.Title = inputParam.Title;

                if (!string.IsNullOrEmpty(inputParam.Text))
                    messageBox.AdjustMaxWidth(inputParam.Text.Length);
                else if (inputParam.Inlines != null)
                {
                    int textLength = 0;
                    CalculateInlineLength(inputParam.Inlines, ref textLength);
                    messageBox.AdjustMaxWidth(textLength);
                }

                if (inputParam.PlaySound)
                    System.Media.SystemSounds.Beep.Play();//new System.Media.SoundPlayer(@"C:\Windows\Media\Windows Ding.wav").Play();

                messageBox.SetCustomText(inputParam);

                messageBox.ShowDialog();
            }));
            if (messageBox == null) return MessageBoxResult.None;
            var messageResult = messageBox.result;
            messageBox = null;
            return messageResult;
        }
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
