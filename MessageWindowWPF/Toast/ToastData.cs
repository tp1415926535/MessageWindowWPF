using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media;

namespace MessageWindowWPF
{
    /// <summary>
    /// Toast Data
    /// </summary>
    public class ToastData
    {
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime Time { get; set; } = DateTime.Now;

        /// <summary>
        /// icon
        /// </summary>
        public ImageSource Icon { get; set; }

        /// <summary>
        /// Title of the window
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Title of notification
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Text Lines
        /// </summary>
        public ObservableCollection<string> Contents { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// Image above the title
        /// </summary>
        public ImageSource HeadImage { get; set; }

        /// <summary>
        /// Image after Text
        /// </summary>
        public ImageSource BodyImage { get; set; }

        /// <summary>
        /// Button List
        /// </summary>
        public ObservableCollection<ButtonActionItem> Buttons { get; set; } = new ObservableCollection<ButtonActionItem>();
        
        /// <summary>
        /// Custom audio for displaying toast
        /// </summary>
        public string CustomAudioPath { get; set; }

        /// <summary>
        /// Seconds of Toast Stay，0 or negative numbers are always displayed.The default value is 7.
        /// </summary>
        public int Duration { get; set; } = 7;
    }

    /// <summary>
    /// button with action
    /// </summary>
    public class ButtonActionItem
    {
        public ButtonActionItem(string text, Action action)
        {
            Text = text;
            Action = action;
        }

        /// <summary>
        /// button content
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// button event
        /// </summary>
        public Action Action { get; set; }
    }

}
