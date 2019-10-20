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
        private MainPageViewModel _ctx = new MainPageViewModel();
        private object _content;

        public MainPage()
        {
            InitializeComponent();
            DataContext = _ctx;
            _content = this.Content;
        }


    }
}
