using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MessageWindowWPF
{
    public class MessageSetting
    {
        private static Dictionary<string, ResourceDictionary> languages = new Dictionary<string, ResourceDictionary>() {
            {"zh",  new ResourceDictionary { Source = new Uri($"/MessageWindowWPF;component/Languages/Chinese.xaml", UriKind.Relative) } },
            {"en",  new ResourceDictionary { Source = new Uri($"/MessageWindowWPF;component/Languages/English.xaml", UriKind.Relative) } },
        };
        private static ResourceDictionary lightTheme = new ResourceDictionary { Source = new Uri($"/MessageWindowWPF;component/Themes/Light.xaml", UriKind.Relative) };
        private static ResourceDictionary darkTheme = new ResourceDictionary { Source = new Uri($"/MessageWindowWPF;component/Themes/Dark.xaml", UriKind.Relative) };

        static MessageSetting()
        {
            UIculture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            UseDarkTheme = false;
        }

        /// <summary>
        /// Show system Header or not
        /// </summary>
        public static bool NoSystemHeader { get; set; }
        /// <summary>
        /// Use conrner Radius
        /// </summary>
        public static bool WithCornerRadius { get; set; }

        /// <summary>
        /// The maximum number of toasts, not limited if it is less than or equal to 0. Default 0
        /// </summary>
        public static int ToastMaxCount { get; set; }

        /// <summary>
        /// Display lastest toast at the bottom. Defualt false.
        /// </summary>
        public static bool ToastLatestAtBottom { get; set; }

        /// <summary>
        /// customColor
        /// </summary>
        private static CustomColorData customColor;
        public static CustomColorData CustomColor
        {
            get { return customColor; }
            set
            {
                if (customColor != null)
                    customColor.PropertyChanged -= CustomColor_PropertyChanged;
                customColor = value;
                UpdateAllColor();
                customColor.PropertyChanged += CustomColor_PropertyChanged;
            }
        }

        /// <summary>
        /// Language Culture
        /// </summary>
        private static CultureInfo culture;
        public static CultureInfo UIculture
        {
            get { return culture; }
            set
            {
                culture = value;
                SwitchLanguage(value);
            }
        }

        /// <summary>
        /// theme
        /// </summary>
        private static bool useDarkTheme;
        public static bool UseDarkTheme
        {
            get { return useDarkTheme; }
            set
            {
                useDarkTheme = value;
                SwitchTheme(value);
            }
        }

        /// <summary>
        /// switch to target Culture language (only English and Chinese)
        /// 切换目标语言（只支持中英文）
        /// </summary>
        /// <param name="cultureInfo"></param>
        public static void SwitchLanguage(CultureInfo cultureInfo)
        {
            if (cultureInfo == null) return;
            if (cultureInfo.Name.Length >= 2)
            {
                var lan = cultureInfo.Name.Substring(0, 2).ToLower();
                if (!languages.ContainsKey(lan))
                    SwitchToExist("en");
                else
                    SwitchToExist(lan);
            }
            else
                SwitchToExist("en");
        }
        private static void SwitchToExist(string lan)
        {
            var dic = Application.Current.Resources.MergedDictionaries;
            foreach (var item in languages)
            {
                if (item.Key.Equals(lan)) continue;
                if (dic.Contains(item.Value))
                    dic.Remove(item.Value);
            }
            if (!dic.Contains(languages[lan]))
                dic.Add(languages[lan]);
        }

        /// <summary>
        /// switchTheme
        /// </summary>
        /// <param name="dark"></param>
        public static void SwitchTheme(bool dark)
        {
            var dict = Application.Current.Resources.MergedDictionaries;
            if (dark == true)
            {
                if (dict.Contains(lightTheme))
                    dict.Remove(lightTheme);
                if (!dict.Contains(darkTheme))
                    dict.Add(darkTheme);
            }
            else
            {
                if (dict.Contains(darkTheme))
                    dict.Remove(darkTheme);
                if (!dict.Contains(lightTheme))
                    dict.Add(lightTheme);
            }
        }

        /// <summary>
        /// custom color change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CustomColor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var p = CustomColor.GetType().GetProperty(e.PropertyName);
            var value = p.GetValue(CustomColor) as Color?;
            if (value == null) return;

            var appResources = Application.Current.Resources;
            if (appResources.Contains(e.PropertyName))
                ((SolidColorBrush)appResources[e.PropertyName]).Color = value.Value;
            else
            {
                SolidColorBrush newBrush = new SolidColorBrush(Colors.Blue);
                appResources.Add(e.PropertyName, newBrush);
            }
        }
        /// <summary>
        /// update all color
        /// </summary>
        public static void UpdateAllColor()
        {
            if (CustomColor == null) return;
            var appResources = Application.Current.Resources;
            foreach (PropertyInfo property in CustomColor.GetType().GetProperties())
            {
                var color = property.GetValue(CustomColor, null) as Color?;
                if (color == null) continue;
                SolidColorBrush newBrush = new SolidColorBrush(color.Value);

                if (appResources.Contains(property.Name))
                    appResources[property.Name] = newBrush;
                else
                    appResources.Add(property.Name, newBrush);
            }
        }

        /// <summary>
        /// Custom color
        /// </summary>
        public class CustomColorData : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            private Color? _windowBack;
            public Color? WindowBack
            {
                get { return _windowBack; }
                set
                {
                    _windowBack = value;
                    NotifyPropertyChanged();
                }
            }

            private Color? _windowText;
            public Color? WindowText
            {
                get { return _windowText; }
                set
                {
                    _windowText = value;
                    NotifyPropertyChanged();
                }
            }

            private Color? _controlBorder;
            public Color? ControlBorder
            {
                get { return _controlBorder; }
                set
                {
                    _controlBorder = value;
                    NotifyPropertyChanged();
                }
            }

            private Color? _buttonBack;
            public Color? ButtonBack
            {
                get { return _buttonBack; }
                set
                {
                    _buttonBack = value;
                    NotifyPropertyChanged();
                }
            }

            private Color? _buttonHover;
            public Color? ButtonHover
            {
                get { return _buttonHover; }
                set
                {
                    _buttonHover = value;
                    NotifyPropertyChanged();
                }
            }

            private Color? _buttonPressed;

            public Color? ButtonPressed
            {
                get { return _buttonPressed; }
                set
                {
                    _buttonPressed = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }


    //public class InvertColorConverter : IValueConverter
    //{
    //    static SolidColorBrush whitebrush = new SolidColorBrush(Colors.White);
    //    static SolidColorBrush blackbrush = new SolidColorBrush(Colors.Black);
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value == null)
    //            return new SolidColorBrush(Colors.Black);
    //        else
    //        {
    //            SolidColorBrush brush = (SolidColorBrush)value;
    //            double grayScale = 0.30 * brush.Color.R + 0.59 * brush.Color.G + 0.11 * brush.Color.B;
    //            if (grayScale > 127)
    //                return blackbrush;
    //            else
    //                return whitebrush;
    //        }
    //    }
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value == null)
    //            return whitebrush;
    //        else
    //        {
    //            SolidColorBrush brush = (SolidColorBrush)value;
    //            if (brush.Equals(blackbrush))
    //                return whitebrush;
    //            else
    //                return blackbrush;
    //        }
    //    }
    //}
}
