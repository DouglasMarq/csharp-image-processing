using BespokeFusion;
using csharp_image_processing.ViewModels;
using Microsoft.Win32;
using OpenCvSharp;
using OpenCvSharp.XFeatures2D;
using System;
using System.IO;
using System.Windows;
using Window = System.Windows.Window;

namespace csharp_image_processing.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //binding context
        private MainWindowViewModel _ctx = new MainWindowViewModel();
        //mainpage
        private MainPage mp;

        //construtor
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _ctx;
            Title = "Autenticação";
            entryLogin.Focus();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //executo a função validadora
            var res = await _ctx.Authenticate(entryLogin.Text, entryPass.Password);
            if (res == "Form")
            {
                MaterialMessageBox.Show("Login e/ou Senha vazio(a).");
            }
            else if (res == "Wrong")
            {
                MaterialMessageBox.Show("Login e/ou Senha incorreto(a).");
            }
            else if (res == "NotFound")
            {
                MaterialMessageBox.Show("Login e/ou Senha não encontrados.");
            }
            else
            {
                //se for OK ele muda o content da página
                mp = new MainPage();
                Width = 800;
                Height = 450;
                Left = (SystemParameters.PrimaryScreenWidth / 2) - (Width / 2);
                Top = (SystemParameters.PrimaryScreenHeight / 2) - (Height / 2);
                Title = "Controle Central";
                Content = mp.Content;
            }
        }

        private async void btnBio_Click(object sender, RoutedEventArgs e)
        {
            var res = await _ctx.AuthenticateBio(ToggleRes.IsChecked);
            if(res == "OK")
            {
                mp = new MainPage();
                Width = 800;
                Height = 450;
                Left = (SystemParameters.PrimaryScreenWidth / 2) - (Width / 2);
                Top = (SystemParameters.PrimaryScreenHeight / 2) - (Height / 2);
                Title = "Controle Central";
                Content = mp.Content;
            }
            else if (res == "Wrong")
            {
                MaterialMessageBox.Show("Digital invalida");
            }
            else if(res == "Canceled")
            {
                //usuario cancelou.
            }
            else
            {
                MaterialMessageBox.Show("Digital invalida");
            }
        }
    }
}
