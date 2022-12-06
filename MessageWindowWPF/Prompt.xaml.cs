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
        DispatcherTimer timer = new DispatcherTimer();

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
        public static Prompt Show(string content, double liveSeconds = 3, Window owner = null, Point? point = null, Color? backColor = null)
        {
            Prompt prompt = null;
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                prompt = new Prompt();
                if (liveSeconds > 0)
                    prompt.timer.Interval = TimeSpan.FromSeconds(liveSeconds);
                else
                    prompt.timer.Stop();
                prompt.MainText.Text = content;

                if (MessageSetting.settings.WithCornerRadius)
                    prompt.MainBorder.CornerRadius = new CornerRadius(5);

                if (content.Length > 1900)
                {
                    double nWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                    double nHieght = System.Windows.SystemParameters.PrimaryScreenHeight;
                    double rate = nWidth / nHieght;

                    double maxW = 1200;
                    var h = content.Length * prompt.MainText.FontSize / maxW * prompt.MainText.FontSize * 1.7 + 150;
                    while (maxW / h < rate)
                    {
                        if (maxW + 100 >= nWidth)
                            break;
                        maxW += 100;
                        h = content.Length * prompt.MainText.FontSize / maxW * prompt.MainText.FontSize * 1.7 + 150;
                    }
                    prompt.MainText.MaxWidth = maxW;
                }

                if (backColor != null)
                {
                    prompt.MainBorder.Background = new SolidColorBrush((Color)backColor);
                    prompt.MainBorder.Background.Opacity = 0.9;
                }
                else if (MessageSetting.settings.BackGroundColor != null)
                {
                    prompt.MainBorder.Background = new SolidColorBrush((Color)MessageSetting.settings.BackGroundColor);
                    prompt.MainBorder.Background.Opacity = 0.9;
                }

                if (point != null)
                {
                    prompt.WindowStartupLocation = WindowStartupLocation.Manual;
                    prompt.Left = ((Point)point).X;
                    prompt.Top = ((Point)point).Y;
                }
                else if (owner != null)
                {
                    prompt.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    prompt.Owner = owner;
                }
                else
                    prompt.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                prompt.Show();
                prompt.MainPopup.IsOpen = true;
                prompt.Activate();
            }));
            return prompt;
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

    }

}
