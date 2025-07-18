using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MessageWindowWPF
{
    /// <summary>
    /// Prompt.xaml 的交互逻辑
    /// </summary>
    public partial class Prompt : Window
    {
        internal Action action { get; set; }

        DispatcherTimer timer { get; set; } = new DispatcherTimer();

        public Prompt()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += AutoClose;
            timer.Start();


            MouseDown += (sender, e) =>
            {
                if (e.ChangedButton != MouseButton.Left) return;
                thumb.RaiseEvent(e);
            };

            thumb.DragDelta += (sender, e) =>
            {
                MainPopup.HorizontalOffset += e.HorizontalChange;
                MainPopup.VerticalOffset += e.VerticalChange;
            };
        }
        private void AdjustMaxWidth(int textLength)
        {
            if (textLength > 1900)
            {
                double nWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double nHieght = System.Windows.SystemParameters.PrimaryScreenHeight;
                double rate = nWidth / nHieght;

                double maxW = 1200;
                var h = textLength * MainText.FontSize / maxW * MainText.FontSize * 1.7 + 150;
                while (maxW / h < rate)
                {
                    if (maxW + 100 >= nWidth)
                        break;
                    maxW += 100;
                    h = textLength * MainText.FontSize / maxW * MainText.FontSize * 1.7 + 150;
                }
                MainText.MaxWidth = maxW;
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
        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            action?.Invoke();
            this.Close();
        }

        private async void AutoClose(object sender, EventArgs e)
        {
            MainPopup.IsOpen = false;
            await Task.Delay(300);
            this.Close();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }


        private static Prompt ShowPrompt(string content = null, IEnumerable<Inline> inlines = null, double liveSeconds = 3, Window owner = null, Point? point = null, Color? backColor = null)
        {
            return ShowPrompt(new PromptParams(content, inlines, liveSeconds, owner, point, backColor));
        }

        private static Prompt ShowPrompt(PromptParams inputParam)
        {
            Prompt prompt = null;
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                prompt = new Prompt();
                if (inputParam.LiveSeconds > 0)
                    prompt.timer.Interval = TimeSpan.FromSeconds(inputParam.LiveSeconds);
                else
                    prompt.timer.Stop();

                if (!string.IsNullOrEmpty(inputParam.Content))
                    prompt.MainText.Text = inputParam.Content;
                if (inputParam.Inlines != null)
                    prompt.MainText.Inlines.AddRange(inputParam.Inlines);

                if (MessageSetting.WithCornerRadius)
                    prompt.MainBorder.CornerRadius = new CornerRadius(5);

                if (!string.IsNullOrEmpty(inputParam.Content))
                    prompt.AdjustMaxWidth(inputParam.Content.Length);
                else if (inputParam.Inlines != null)
                {
                    int textLength = 0;
                    CalculateInlineLength(inputParam.Inlines, ref textLength);
                    prompt.AdjustMaxWidth(textLength);
                }

                if (inputParam.BackColor != null)
                {
                    prompt.MainBorder.Background = new SolidColorBrush(inputParam.BackColor.Value);
                    if (inputParam.BackOpacity != null)
                        prompt.MainBorder.Background.Opacity = inputParam.BackOpacity.Value;
                    else
                        prompt.MainBorder.Background.Opacity = 0.9;
                }

                if (inputParam.ForeColor != null)
                    prompt.MainText.Foreground = new SolidColorBrush(inputParam.ForeColor.Value);

                if (inputParam.Location != null)
                {
                    prompt.WindowStartupLocation = WindowStartupLocation.Manual;
                    prompt.Left = inputParam.Location.Value.X;
                    prompt.Top = inputParam.Location.Value.Y;
                }
                else if (inputParam.Owner != null)
                {
                    prompt.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    prompt.Owner = inputParam.Owner;
                }
                else
                    prompt.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                if (inputParam.Action != null)
                {
                    prompt.action = inputParam.Action;
                    prompt.ActionButton.Visibility = Visibility.Visible;
                    if (!string.IsNullOrEmpty(inputParam.ActionText))
                        prompt.ActionText.Text = inputParam.ActionText;
                    if (inputParam.ActionForeColor != null)
                        prompt.ActionText.Foreground = new SolidColorBrush(inputParam.ActionForeColor.Value);
                }

                prompt.Show();
                prompt.MainPopup.IsOpen = true;
                prompt.Activate();
            }));
            return prompt;

        }

    }
}
