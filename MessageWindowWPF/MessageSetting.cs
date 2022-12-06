using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MessageWindowWPF
{
    public class MessageSetting
    {
        static Dictionary<string, ResourceDictionary> languages = new Dictionary<string, ResourceDictionary>() {
            {"zh",  new ResourceDictionary { Source = new Uri($"/MessageWindowWPF;component/Languages/Chinese.xaml", UriKind.Relative) } },
            {"en",  new ResourceDictionary { Source = new Uri($"/MessageWindowWPF;component/Languages/English.xaml", UriKind.Relative) } },
        };
        public static Settings settings = new Settings() { UIculture = System.Threading.Thread.CurrentThread.CurrentUICulture };


        public class Settings
        {
            public bool NoSystemHeader { get; set; }
            public bool WithCornerRadius { get; set; }
            public Color? BackGroundColor { get; set; }

            private CultureInfo culture;
            public CultureInfo UIculture
            {
                get { return culture; }
                set
                {
                    culture = value;
                    SwitchLanguage(value);
                }
            }
        }


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
    }



    public class InvertColorConverter : IValueConverter
    {
        static SolidColorBrush whitebrush = new SolidColorBrush(Colors.White);
        static SolidColorBrush blackbrush = new SolidColorBrush(Colors.Black);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new SolidColorBrush(Colors.Black);
            else
            {
                SolidColorBrush brush = (SolidColorBrush)value;
                double grayScale = 0.30 * brush.Color.R + 0.59 * brush.Color.G + 0.11 * brush.Color.B;
                if (grayScale > 127)
                    return blackbrush;
                else
                    return whitebrush;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return whitebrush;
            else
            {
                SolidColorBrush brush = (SolidColorBrush)value;
                if (brush.Equals(blackbrush))
                    return whitebrush;
                else
                    return blackbrush;
            }
        }
    }
}
