using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
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
    }
}
