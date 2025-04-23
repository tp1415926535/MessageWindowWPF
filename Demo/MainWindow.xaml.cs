using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MessageWindowWPF;
using MessageBox = MessageWindowWPF.MessageBox;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int buttonIndex = 0;
        bool showRichText = false;

        public MainWindow()
        {
            InitializeComponent();

            new ToastWindowBuilder()
               .SetTitle("app name")
               .SetIcon(new Uri("/Resources/logo.png", UriKind.RelativeOrAbsolute))
               .SetHeader("Test Window Show")
               .AddContent("Click Button to test.")
               .Show();
            //MessageSetting.ToastMaxCount = 3;
            //MessageSetting.ToastLatestAtBottom = true;
            //MessageSetting.ToastDoubleClickClose = true;
        }

        private void ButtonEvents(bool taskThread = false)
        {
            switch (buttonIndex)
            {
                case 1:
                    if (showRichText)
                    {
                        List<Inline> inlines = new List<Inline>();
                        inlines.Add(new Run("normal text. "));
                        inlines.Add(new Run("red text. ") { Foreground = Brushes.Red });
                        inlines.Add(new LineBreak());
                        inlines.Add(new Bold(new Run("Second Line.")) { Foreground = Brushes.Blue, FontSize = 20 });
                        MessageBox.Show(inlines, "Tip", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show(taskThread ? "Task Thread" : "Foregound Thread", "Tip", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 2:
                    InputBox inputBox = new InputBox();
                    if (inputBox.ShowDialog("Write Something:", "Title") == true)
                    {
                        if (!string.IsNullOrEmpty(inputBox.value))
                            MessageBox.Show(inputBox.value);
                        else
                            MessageBox.Show("Nothing input!");
                    }
                    else
                        MessageBox.Show("Cancel Input!");
                    break;
                case 3:
                    if (showRichText)
                    {
                        List<Inline> inlines = new List<Inline>();
                        inlines.Add(new Run("normal text. "));
                        inlines.Add(new Run("red text. ") { Foreground = Brushes.Red });
                        inlines.Add(new LineBreak());
                        inlines.Add(new Bold(new Run("Second Line.")) { Foreground = Brushes.Blue, FontSize = 20 });
                        Prompt.Show(inlines);
                    }
                    else
                        Prompt.Show("Prompt text here, it will auto close after 3 seconds");
                    break;
            }
        }


        private void MessageButton_Click(object sender, RoutedEventArgs e)
        {
            buttonIndex = 1;
            if (CheckboxThread.IsChecked == true)
            {
                if (showRichText)
                {
                    MessageBox.Show("Diffrent thread can't pass UI elements!", "Tip", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return;
                }
                Task.Run(() =>
                {
                    ButtonEvents(true);
                });
            }
            else
                ButtonEvents();
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            buttonIndex = 2;
            if (CheckboxThread.IsChecked == true)
            {
                Task.Run(() =>
                {
                    ButtonEvents(true);
                });
            }
            else
                ButtonEvents();
        }

        private void PromptButton_Click(object sender, RoutedEventArgs e)
        {
            buttonIndex = 3;
            if (CheckboxThread.IsChecked == true)
            {
                if (showRichText)
                {
                    Prompt.Show("Diffrent thread can't pass UI elements!");
                    return;
                }
                Task.Run(() =>
                {
                    ButtonEvents(true);
                });
            }
            else
                ButtonEvents();
        }

        private void CheckboxHeader_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            MessageSetting.NoSystemHeader = (bool)checkBox.IsChecked;
        }

        private void CheckboxRadius_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            MessageSetting.WithCornerRadius = (bool)checkBox.IsChecked;
        }
        private void CheckboxRich_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            showRichText = (bool)checkBox.IsChecked;
        }

        private void CheckboxTheme_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            MessageSetting.UseDarkTheme = (bool)checkBox.IsChecked;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageSetting.UIculture = new CultureInfo("en-US");
            //MessageSetting.CustomColor = new MessageSetting.CustomColorData() { WindowText = Colors.Red };
        }

        private void ToastButton_Click(object sender, RoutedEventArgs e)
        {
            var builder = new ToastWindowBuilder()
                .SetTitle("app name")
                .SetIcon(new Uri("/Resources/logo.png", UriKind.RelativeOrAbsolute))
                .SetHeader("ToastTitle");
                //.KeepDisplay()
                //.Mute();
            Random random = new Random();
            int randomNumber = random.Next(0, 6);
            switch (randomNumber)
            {
                case 0:
                    builder.SetBodyImage(new Uri("/Resources/pexels-maria-mileta.jpg", UriKind.RelativeOrAbsolute))
                            .AddContent("line1: some content here.")
                            .AddContent("line2: nothing.");
                    break;
                case 1:
                    builder.SetHeadImage(new Uri("/Resources/pexels-maria-mileta.jpg", UriKind.RelativeOrAbsolute))
                           .AddContent("line1: some content here.")
                           .AddContent("line2: nothing.");
                    break;
                case 2:
                    builder.AddContent("line1: some content here.")
                           .AddContent("line2: nothing.");
                    break;
                case 3:
                    builder.AddContent("line1: some content here.")
                           .AddButton("hello", ShowHello)
                           .AddButton("goodbye", ShowGoodbye);
                    break;
                case 4:
                    builder.SetHeadImage(new BitmapImage(new Uri("/Resources/pexels-maria-mileta.jpg", UriKind.RelativeOrAbsolute)))
                           .AddContent("line1: some content here.")
                           .AddContent("line2: nothing.")
                           .AddButton("hello", ShowHello)
                           .AddButton("goodbye", ShowGoodbye);
                    break;
                case 5:
                    builder.SetBodyImage(new BitmapImage(new Uri("/Resources/pexels-maria-mileta.jpg", UriKind.RelativeOrAbsolute)))
                           .AddContent("line1: some content here.")
                           .AddContent("line2: nothing.")
                           .AddButton("hello", ShowHello)
                           .AddButton("goodbye", ShowGoodbye);
                    break;
            }
            builder.Show();
            /*var toastData = new ToastData()
            {
                Title = "demo",
                Header = "ToastTitle",
                Icon = new BitmapImage(new Uri("/Resources/logo.png", UriKind.RelativeOrAbsolute)),
                //Time = string.Empty,
                //Duration = 0
            };
            Random random = new Random();
            int randomNumber = random.Next(0, 6);
            switch (randomNumber)
            {
                case 0:
                    toastData.BodyImage = new BitmapImage(new Uri("/Resources/pexels-maria-mileta.jpg", UriKind.RelativeOrAbsolute));
                    toastData.Contents.Add("line1: some content here.");
                    toastData.Contents.Add("line2: nothing.");
                    break;
                case 1:
                    toastData.HeadImage = new BitmapImage(new Uri("/Resources/pexels-maria-mileta.jpg", UriKind.RelativeOrAbsolute));
                    toastData.Contents.Add("line1: some content here.");
                    toastData.Contents.Add("line2: nothing.");
                    break;
                case 2:
                    toastData.Contents.Add("line1: some content here.");
                    toastData.Contents.Add("line2: nothing.");
                    break;
                case 3:
                    toastData.Contents.Add("line1: some content here.");
                    toastData.Buttons.Add(new ButtonActionItem("hello", ShowHello));
                    toastData.Buttons.Add(new ButtonActionItem("goodbye", ShowGoodbye));
                    break;
                case 4:
                    toastData.HeadImage = new BitmapImage(new Uri("/Resources/pexels-maria-mileta.jpg", UriKind.RelativeOrAbsolute));
                    toastData.Contents.Add("line1: some content here.");
                    toastData.Contents.Add("line2: nothing.");
                    toastData.Buttons.Add(new ButtonActionItem("hello", ShowHello));
                    toastData.Buttons.Add(new ButtonActionItem("goodbye", ShowGoodbye));
                    break;
                case 5:
                    toastData.BodyImage = new BitmapImage(new Uri("/Resources/pexels-maria-mileta.jpg", UriKind.RelativeOrAbsolute));
                    toastData.Contents.Add("line1: some content here.");
                    toastData.Contents.Add("line2: nothing.");
                    toastData.Buttons.Add(new ButtonActionItem("hello", ShowHello));
                    toastData.Buttons.Add(new ButtonActionItem("goodbye", ShowGoodbye));
                    break;
            }
            ToastWindow.Show(toastData);*/
        }


        private void ShowHello()
        {
            MessageBox.Show("hello button has been clicked");
        }
        private void ShowGoodbye()
        {
            MessageBox.Show("goodbye button has been clicked");
        }
    }
}
