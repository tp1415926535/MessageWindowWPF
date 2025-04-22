using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageWindowWPF
{
    class ToastListManager
    {
        /// <summary>
        /// Toast window list
        /// </summary>
        static List<ToastWindow> windowList { get; set; } = new List<ToastWindow>();

        /// <summary>
        /// window spacing
        /// </summary>
        static int spacing { get; set; } = 10;

        /// <summary>
        /// Add to list
        /// </summary>
        /// <param name="window"></param>
        /// <returns>return top offset</returns>
        public static double AppendWindow(ToastWindow window)
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
            var index = windowList.IndexOf(window);
            if (index < 0) return;
            windowList.RemoveAt(index);
            var height = window.Height;
            if (index > windowList.Count - 1) return;
            for (int i = index; i < windowList.Count; i++)//The subsequent window moves downwards
                windowList[i].Top += height + spacing;
        }
    }
}
