using csharp_image_processing.ViewModels;
using System.Windows;

namespace csharp_image_processing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel ctx = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ctx;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
