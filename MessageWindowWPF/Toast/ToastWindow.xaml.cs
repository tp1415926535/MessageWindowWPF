using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MessageWindowWPF
{
    /// <summary>
    /// ToastWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ToastWindow : Window
    {
        /// <summary>
        /// Has the close animation already been played
        /// </summary>
        bool hasPlayedAni { get; set; }

        /// <summary>
        /// data
        /// </summary>
        ToastData data { get; set; }

        public ToastWindow(ToastData toastData)
        {
            this.DataContext = data = toastData;
            InitializeComponent();
        }

        /// <summary>
        /// Window loading completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Fade in animation
            var storyboard = this.FindResource("ShowWindowStoryboard") as Storyboard;
            storyboard?.Begin(this);

            //Window basic setting
            this.SetBlur(true);
            this.Top = System.Windows.SystemParameters.WorkArea.Height - this.Height - 10;
            this.Left = System.Windows.SystemParameters.WorkArea.Width - this.Width - 10;
            this.Top -= ToastListManager.AddWindowList(this);

            //Play sound
            if (!data.Mute)
            {
                if (!string.IsNullOrEmpty(data.CustomAudioPath) && File.Exists(data.CustomAudioPath))
                {
                    try
                    {
                        using (var player = new SoundPlayer(data.CustomAudioPath))
                        {
                            player.Play();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                    }
                }
                else
                {
                    var wav = @"C:\Windows\Media\Windows Notify System Generic.wav";
                    if (File.Exists(wav))
                    {
                        using (var player = new SoundPlayer(wav))
                        {
                            player.Play();
                        }
                    }
                }
            }
            //Automatically close the window
            if (data.Duration <= 0) return;
            await Task.Delay(data.Duration * 1000);
            this.Close();
        }

        /// <summary>
        /// Close button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Window close event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!hasPlayedAni)
                e.Cancel = true;
            else
                ToastListManager.RemoveWindow(this);

            //Fade out animation
            var storyboard = this.FindResource("HideWindowStoryboard") as Storyboard;
            storyboard?.Begin(this);

            //Close after playing is complete
            await Task.Delay(200);
            hasPlayedAni = true;
            this.Close();
        }

        /// <summary>
        /// Response button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;
            var item = button.DataContext as ButtonActionItem;
            if (item == null) return;

            this.Close();
            Task.Run(() =>
            {
                item.Action?.Invoke();
            });
        }
        /// <summary>
        /// Double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2) return;
            if (MessageSetting.ToastDoubleClickClose)
                this.Close();
        }


        /// <summary>
        /// Public static method. Recommend.
        /// </summary>
        /// <param name="data"></param>
        public static void Show(ToastData data)
        {
            ToastListManager.CreateOrWait(data);
        }

    }
}
