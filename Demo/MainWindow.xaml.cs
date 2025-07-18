using MessageWindowWPF;
using System.Globalization;
using System.Windows;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel viewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel = new MainViewModel();

            //MessageSetting.UIculture = new CultureInfo("en-US");
        }
    }
}
