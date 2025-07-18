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
        #region properties
        /// <summary>
        /// Input result value
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// Max input length
        /// </summary>
        public int MaxLength
        {
            get { return _maxLength; }
            set
            {
                _maxLength = value;
                ValueText.MaxLength = value;
                PasswordText.MaxLength = value;
            }
        }
        private int _maxLength;
        /// <summary>
        /// Display password box when the value is not empty
        /// </summary>
        public char? PasswordChar
        {
            get { return _passwordChar; }
            set
            {
                _passwordChar = value;
                if (value != null)
                {
                    PasswordText.Visibility = Visibility.Visible;
                    ValueText.Visibility = Visibility.Hidden;
                    PasswordText.PasswordChar = value.Value;//●
                }
                else
                {
                    PasswordText.Visibility = Visibility.Hidden;
                    ValueText.Visibility = Visibility.Visible;
                }
            }
        }
        private char? _passwordChar;
        #endregion;

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
            if (MessageSetting.NoSystemHeader)
            {
                this.WindowStyle = WindowStyle.None;
                this.AllowsTransparency = true;
                MainBorder.Margin = new Thickness(12);
                MainBorder.BorderThickness = new Thickness(1);
                MainBorder.Effect = (System.Windows.Media.Effects.Effect)Resources["shadowEffect"];
                if (MessageSetting.WithCornerRadius)
                    MainBorder.CornerRadius = new CornerRadius(5);
            }
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
            if (PasswordChar != null)
                value = PasswordText.Password;
            else
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
