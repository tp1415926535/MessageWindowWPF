using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageWindowWPF;
using MessageBox = MessageWindowWPF.MessageBox;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        static int buttonIndex = 0;

        public MainWindow()
        {
            InitializeComponent();

            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
        }

        private void ButtonEvents()
        {
            switch (buttonIndex)
            {
                case 1:
                    MessageBox.Show("Foregound Thread", "Tip", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 2:
                    InputBox inputBox = new InputBox();
                    if (inputBox.ShowDialog("Write Something:", "Title") == true)
                        MessageBox.Show(inputBox.value);
                    else
                        MessageBox.Show("Cancel Input!");
                    break;
                case 3:
                    Prompt.Show("Prompt text here, it will auto close after 3 seconds");
                    break;
            }
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            switch (buttonIndex)
            {
                case 1:
                    MessageBox.Show("Background Thread", "Tip", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    break;
                case 2:
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        InputBox inputBox = new InputBox();
                        if (inputBox.ShowDialog("Write Something:", "Title") == true)
                            MessageBox.Show(inputBox.value);
                        else
                            MessageBox.Show("Cancel Input!");
                    }));
                    break;
                case 3:
                    Prompt.Show("Background Thread. Double click to close it.");
                    break;
            }
        }

        private void MessageButton1_Click(object sender, RoutedEventArgs e)
        {
            buttonIndex = 1;
            ButtonEvents();
        }

        private void MessageButton2_Click(object sender, RoutedEventArgs e)
        {
            buttonIndex = 1;
            if (backgroundWorker.IsBusy != true)
                backgroundWorker.RunWorkerAsync();
        }

        private void InputButton1_Click(object sender, RoutedEventArgs e)
        {
            buttonIndex = 2;
            ButtonEvents();
        }

        private void InputButton2_Click(object sender, RoutedEventArgs e)
        {
            buttonIndex = 2;
            if (backgroundWorker.IsBusy != true)
                backgroundWorker.RunWorkerAsync();
        }

        private void PromptButton1_Click(object sender, RoutedEventArgs e)
        {
            buttonIndex = 3;
            ButtonEvents();
        }

        private void PromptButton2_Click(object sender, RoutedEventArgs e)
        {
            buttonIndex = 3;
            if (backgroundWorker.IsBusy != true)
                backgroundWorker.RunWorkerAsync();
        }

        private void Checkbox1_Checked(object sender, RoutedEventArgs e)
        {
            MessageSetting.settings.NoSystemHeader = (bool)Checkbox1.IsChecked;
        }

        private void Checkbox2_Checked(object sender, RoutedEventArgs e)
        {
            MessageSetting.settings.WithCornerRadius = (bool)Checkbox2.IsChecked;
        }
    }
}
