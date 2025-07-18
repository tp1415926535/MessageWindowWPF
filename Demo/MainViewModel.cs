using CommunityToolkit.Mvvm.Input;
using MessageWindowWPF;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using MessageBox = MessageWindowWPF.MessageBox;

namespace Demo
{
    [AddINotifyPropertyChangedInterface]
    internal partial class MainViewModel
    {
        public bool IsRichText { get; set; }

        public bool IsBackThread { get; set; }

        public bool IsDarkTheme { get; set; }

        public bool NoSystemHeader { get; set; }

        public bool WithCornerRadius { get; set; }

        public bool IsPasswordInput { get; set; }

        public bool IsCustomMsgButtonText { get; set; }

        public bool IsPromptWithAction { get; set; }

        public bool ToastLatestAtBottom { get; set; }

        [RelayCommand]
        public void ShowMessageBox()
        {
            DoAction(() =>
            {
                if (IsRichText)
                {
                    List<Inline> inlines = new List<Inline>();
                    if (IsBackThread)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            inlines.Add(new Run("UI elements"));
                            inlines.Add(new LineBreak());
                            inlines.Add(new Run("need") { Foreground = Brushes.DeepSkyBlue, FontWeight = FontWeights.Bold });
                            inlines.Add(new LineBreak());
                            inlines.Add(new Run("Dispatcher invoke. ") { Foreground = Brushes.Red });
                        });
                    }
                    else
                    {
                        inlines.Add(new Run("normal text. "));
                        inlines.Add(new Run("red text. ") { Foreground = Brushes.Red });
                        inlines.Add(new LineBreak());
                        inlines.Add(new Bold(new Run("Second Line.")) { Foreground = Brushes.Blue, FontSize = 20 });
                    }
                    if (IsCustomMsgButtonText)
                        MessageBox.Show(new MessageBoxParams() { Inlines = inlines, Title = "Tip", Icon = MessageBoxImage.Information, CustomOkText = "CustomButtonText" });
                    else
                        MessageBox.Show(inlines, "Tip", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (IsCustomMsgButtonText)
                        MessageBox.Show(new MessageBoxParams() { Text = IsBackThread ? "Task Thread" : "UI Thread", Title = "Tip", Icon = MessageBoxImage.Information, CustomOkText = "CustomButtonText" });
                    else
                        MessageBox.Show(IsBackThread ? "Task Thread" : "UI Thread", "Tip", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }

        [RelayCommand]
        public void ShowInputBox()
        {
            InputBox inputBox = new InputBox();
            if (IsPasswordInput)
                inputBox.PasswordChar = '●';
            if (inputBox.ShowDialog("Write Something:", "Title") == true)
            {
                if (!string.IsNullOrEmpty(inputBox.value))
                    MessageBox.Show(inputBox.value, "Result");
                else
                    MessageBox.Show("Nothing input!", "Result");
            }
            else
                MessageBox.Show("Cancel Input!", "Result");
        }

        [RelayCommand]
        public void ShowPrompt()
        {
            DoAction(() =>
            {
                if (IsRichText)
                {
                    List<Inline> inlines = new List<Inline>();
                    if (IsBackThread)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            inlines.Add(new Run("UI elements"));
                            inlines.Add(new LineBreak());
                            inlines.Add(new Run("need") { Foreground = Brushes.DeepSkyBlue, FontWeight = FontWeights.Bold });
                            inlines.Add(new LineBreak());
                            inlines.Add(new Run("Dispatcher invoke. ") { Foreground = Brushes.Red });
                        });
                    }
                    else
                    {
                        inlines.Add(new Run("normal text. "));
                        inlines.Add(new Run("red text. ") { Foreground = Brushes.Red });
                        inlines.Add(new LineBreak());
                        inlines.Add(new Bold(new Run("Second Line.")) { Foreground = Brushes.Blue, FontSize = 20 });
                    }
                    if (IsPromptWithAction)
                        Prompt.Show(new PromptParams()
                        {
                            Inlines = inlines,
                            ActionText = "Click Here",
                            Action = (() =>
                            {
                                MessageBox.Show("ClickPrompt Event");
                            })
                        });
                    else
                        Prompt.Show(inlines);
                }
                else
                {
                    if (IsPromptWithAction)
                        Prompt.Show(new PromptParams()
                        {
                            Content = "Prompt text here, it will auto close after 3 seconds",
                            ActionText = "Click Here",
                            Action = (() =>
                            {
                                MessageBox.Show("ClickPrompt Event");
                            }),
                            ActionForeColor = Colors.Orange
                        });
                    else
                        Prompt.Show("Prompt text here, it will auto close after 3 seconds");
                }
            });
        }

        [RelayCommand]
        public void ShowToastWindow()
        {
            var builder = new ToastWindowBuilder()
                .SetTitle("app name")
                .SetIcon(new Uri("/Resources/logo.png", UriKind.RelativeOrAbsolute))
                .SetHeader("ToastTitle");
            //.KeepDisplay()
            //.Mute();
            Random random = new Random();
            if (random.Next(0, 2) > 0)
                builder.SetLargeIcon(new Uri("/Resources/blank-profile-male.jpg", UriKind.RelativeOrAbsolute), true);
            if (random.Next(0, 2) > 0)
                builder.AddContent("line1: some content here.");
            if (random.Next(0, 2) > 0)
                builder.AddContent("line2: another content.");
            if (random.Next(0, 10) > 6)
                builder.AddContent("line3: random word.");
            if (random.Next(0, 10) > 8)
                builder.AddContent("line4: nothing.");
            if (random.Next(0, 2) > 0)
                builder.AddButton("hello", ShowHello)
                       .AddButton("goodbye", ShowGoodbye);

            var randomNum = random.Next(0, 3);
            if (randomNum == 0)
                builder.SetBodyImage(new Uri("/Resources/pexels-maria-mileta.jpg", UriKind.RelativeOrAbsolute));
            else if (randomNum == 1)
                builder.SetHeadImage(new Uri("/Resources/pexels-maria-mileta.jpg", UriKind.RelativeOrAbsolute));

            builder.Show();

            //new ToastWindowBuilder()
            //   .SetTitle("app name")
            //   .SetIcon(new Uri("/Resources/logo.png", UriKind.RelativeOrAbsolute))
            //   .SetHeader("Test Window Show")
            //   .AddContent("Click Button to test.")
            //   .Show();
        }
        private void ShowHello()
        {
            MessageBox.Show("hello button has been clicked");
        }
        private void ShowGoodbye()
        {
            MessageBox.Show("goodbye button has been clicked");
        }

        private void DoAction(Action action)
        {
            if (IsBackThread)
                Task.Run(() => { action?.Invoke(); });
            else
                action?.Invoke();
        }




        [RelayCommand]
        public void SwitchNoSysHeader()
        {
            MessageSetting.NoSystemHeader = NoSystemHeader;
        }

        [RelayCommand]
        public void SwitchWithCornerRadius()
        {
            MessageSetting.WithCornerRadius = WithCornerRadius;
        }

        [RelayCommand]
        public void SwitchIsDarkTheme()
        {
            MessageSetting.UseDarkTheme = IsDarkTheme;
        }

        [RelayCommand]
        public void SwitchToastDirection()
        {
            MessageSetting.ToastLatestAtBottom = ToastLatestAtBottom;
        }
    }
}
