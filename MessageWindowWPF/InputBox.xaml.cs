using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MessageWindowWPF
{
    /// <summary>
    /// InputBox.xaml 的交互逻辑
    /// </summary>
    public partial class InputBox : Window
    {
        public string value;
        public InputBox()
        {
            InitializeComponent();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void SetWindowStyle()
        {
            if (MessageSetting.settings.NoSystemHeader)
            {
                this.WindowStyle = WindowStyle.None;
                this.AllowsTransparency = true;
                MainBorder.Margin = new Thickness(12);
                MainBorder.BorderThickness = new Thickness(1);
                MainBorder.Effect = (System.Windows.Media.Effects.Effect)Resources["shadowEffect"];
                if (MessageSetting.settings.WithCornerRadius)
                    MainBorder.CornerRadius = new CornerRadius(5);
            }
            if (MessageSetting.settings.BackGroundColor != null)
                MainBorder.Background = new SolidColorBrush((Color)MessageSetting.settings.BackGroundColor);
        }

        public bool? ShowDialog(string message = null, string title = null, string defaultValue = null)
        {
            SetWindowStyle();
            if (!string.IsNullOrEmpty(message)) PromptText.Text = message;
            if (!string.IsNullOrEmpty(title)) this.Title = title;
            if (!string.IsNullOrEmpty(defaultValue)) ValueText.Text = defaultValue;
            ValueText.Focus();
            return base.ShowDialog();
        }


        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            value = ValueText.Text;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
