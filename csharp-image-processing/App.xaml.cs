﻿using csharp_image_processing.Model.Database;
using csharp_image_processing.Model.Entidades;
using System.Windows;

namespace csharp_image_processing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //verifico se o banco tem alguma entrada
            if (new AccessLogin().Count() == 0)
            {
                //objeto que vai inserir as credenciais no banco
                Login lg = new Login()
                {
                    Id = new AccessLogin().Count() + 1,
                    User = "admin",
                    Pass = "1234",
                    AccessLevel = 3,
                };
                //inserção da credencial no banco
                new AccessLogin().Insert(lg);
            }
            base.OnStartup(e);
        }
    }
}
