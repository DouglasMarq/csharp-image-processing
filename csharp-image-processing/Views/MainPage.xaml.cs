using csharp_image_processing.ViewModels;
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

namespace csharp_image_processing.Views
{
    /// <summary>
    /// Lógica interna para MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        private MainPageViewModel _ctx;
        private static string _user;
        private static string _pass;
        public event EventHandler ChangeContent;

        public MainPage(string user, string pass)
        {
            InitializeComponent();
            _user = user;
            _pass = pass;
            _ctx = new MainPageViewModel(_user, _pass);
            DataContext = _ctx;
            switch (_ctx.AccessLevel)
            {
                case 1:
                    XpanderLevelOne.Visibility = Visibility.Visible;
                    XpanderLevelTwo.Height = 0;
                    XpanderLevelTwo.Visibility = Visibility.Hidden;
                    XpanderLevelThree.Height = 0;
                    XpanderLevelThree.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    XpanderLevelOne.Visibility = Visibility.Visible;
                    XpanderLevelTwo.Visibility = Visibility.Visible;
                    XpanderLevelThree.Height = 0;
                    XpanderLevelThree.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    XpanderLevelOne.Visibility = Visibility.Visible;
                    XpanderLevelTwo.Visibility = Visibility.Visible;
                    XpanderLevelThree.Visibility = Visibility.Visible;
                    break;
                default:
                    XpanderLevelOne.Visibility = Visibility.Visible;
                    XpanderLevelTwo.Height = 0;
                    XpanderLevelTwo.Visibility = Visibility.Hidden;
                    XpanderLevelThree.Height = 0;
                    XpanderLevelThree.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            ChangeContent?.Invoke(this, null);
        }
    }
}
