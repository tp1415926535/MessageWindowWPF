using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MessageWindowWPF
{
    /// <summary>
    /// Build toast helper class to simplify code
    /// </summary>
    public class ToastWindowBuilder
    {
        /// <summary>
        /// Stored data
        /// </summary>
        ToastData toastData { get; set; }

        public ToastWindowBuilder()
        {
            toastData = new ToastData();
        }

        /// <summary>
        /// Display the notification creation time Default is Now.ToString("yyyy-MM-dd HH:mm:ss")
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetTime(string time)
        {
            toastData.Time = time;
            return this;
        }

        /// <summary>
        /// Set small app icon for notifications
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetIcon(Uri uri)
        {
            toastData.Icon = new BitmapImage(uri);
            return this;
        }

        /// <summary>
        /// Set small app icon for notifications
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetIcon(ImageSource img)
        {
            toastData.Icon = img;
            return this;
        }

        /// <summary>
        /// Set large icon for notifications
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isCircle"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetLargeIcon(Uri uri, bool isCircle = false)
        {
            toastData.LargeIcon = new BitmapImage(uri);
            toastData.IsCircleLargeIcon = isCircle;
            return this;
        }

        /// <summary>
        /// Set large icon for notifications
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isCircle"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetLargeIcon(ImageSource img, bool isCircle = false)
        {
            toastData.LargeIcon = img;
            toastData.IsCircleLargeIcon = isCircle;
            return this;
        }

        /// <summary>
        ///  Set app title for notifications
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetTitle(string text)
        {
            toastData.Title = text;
            return this;
        }

        /// <summary>
        ///  Set bold title for notifications
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetHeader(string text)
        {
            toastData.Header = text;
            return this;
        }

        /// <summary>
        /// Add notification details, add a single line
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public ToastWindowBuilder AddContent(string text)
        {
            toastData.Contents.Add(text);
            return this;
        }

        /// <summary>
        /// Set an image above the title
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetHeadImage(Uri uri)
        {
            toastData.HeadImage = new BitmapImage(uri);
            return this;
        }

        /// <summary>
        /// Set an image above the title
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetHeadImage(ImageSource img)
        {
            toastData.HeadImage = img;
            return this;
        }

        /// <summary>
        /// Set an image below the content and above the button.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetBodyImage(Uri uri)
        {
            toastData.BodyImage = new BitmapImage(uri);
            return this;
        }

        /// <summary>
        /// Set an image below the content and above the button.
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetBodyImage(ImageSource img)
        {
            toastData.BodyImage = img;
            return this;
        }

        /// <summary>
        /// Add button, including its text and event.
        /// All buttons are on one line with equal width
        /// </summary>
        /// <param name="text"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public ToastWindowBuilder AddButton(string text, Action action)
        {
            toastData.Buttons.Add(new ButtonActionItem(text, action));
            return this;
        }

        /// <summary>
        /// Set custom audio to play when toast show
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetAudioPath(string file)
        {
            toastData.CustomAudioPath = file;
            return this;
        }

        /// <summary>
        /// Notification duration, default is 7 seconds. 
        /// If it is less than or equal to 0, it will always be displayed until manually turned off
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public ToastWindowBuilder SetDuration(int seconds)
        {
            toastData.Duration = seconds;
            return this;
        }

        /// <summary>
        /// Displayed until manually turned off
        /// </summary>
        /// <returns></returns>
        public ToastWindowBuilder KeepDisplay()
        {
            toastData.Duration = 0;
            return this;
        }

        /// <summary>
        /// Do not play audio when toast show.
        /// </summary>
        /// <returns></returns>
        public ToastWindowBuilder Mute()
        {
            toastData.Mute = true;
            return this;
        }


        /// <summary>
        /// Ready to show toast
        /// </summary>
        public void Show()
        {
            ToastListManager.CreateOrWait(toastData);
        }
    }
}
