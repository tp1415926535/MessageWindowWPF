using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageWindowWPF
{
    class ToastListManager
    {
        /// <summary>
        /// Toast window list
        /// </summary>
        static List<ToastWindow> windowList { get; set; } = new List<ToastWindow>();

        /// <summary>
        /// 
        /// </summary>
        static Queue<ToastData> waitQueue { get; set; } = new Queue<ToastData>();

        /// <summary>
        /// window spacing
        /// </summary>
        static int spacing { get; set; } = 10;

        /// <summary>
        /// Add to list
        /// </summary>
        /// <param name="window"></param>
        /// <returns>return top offset</returns>
        public static double AddWindowList(ToastWindow window)
        {
            var allHeight = windowList.Sum(x => x.Height);
            var delta = allHeight + windowList.Count * spacing;
            windowList.Add(window);
            return delta;
        }
        /// <summary>
        /// Remove from list
        /// </summary>
        public static void RemoveWindow(ToastWindow window)
        {
            RemoveCurrentWindow(window);
            if (waitQueue.Count > 0)
            {
                var data = waitQueue.Dequeue();
                ToastWindow toastWindow = new ToastWindow(data);
                toastWindow.Show();
            }
        }
        private static void RemoveCurrentWindow(ToastWindow window)
        {
            var index = windowList.IndexOf(window);
            if (index < 0) return;
            windowList.RemoveAt(index);

            var height = window.Height;
            if (index > windowList.Count - 1) return;
            for (int i = index; i < windowList.Count; i++)//The subsequent window moves downwards
                windowList[i].Top += height + spacing;
        }

        /// <summary>
        /// Check if exist window has reached the upper limit
        /// </summary>
        /// <param name="data"></param>
        public static void CreateOrWait(ToastData data)
        {
            if (MessageSetting.ToastMaxCount > 0)
            {
                if (windowList.Count >= MessageSetting.ToastMaxCount)
                {
                    waitQueue.Enqueue(data);
                    return;
                }
            }
            ToastWindow toastWindow = new ToastWindow(data);
            toastWindow.Show();
        }
    }
}
