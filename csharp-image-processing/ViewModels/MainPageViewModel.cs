using csharp_image_processing.Model.Database;
using csharp_image_processing.Model.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_image_processing.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private int _accessLevel;

        private string _authUser;
        public string AuthUser
        {
            get { return _authUser; }
            set { SetProperty(ref _authUser, value); }
        }

        private string _authPass;
        public string AuthPass
        {
            get { return _authPass; }
            set { SetProperty(ref _authPass, value); }
        }

        public int AccessLevel
        {
            get { return _accessLevel; }
            set { SetProperty(ref _accessLevel, value); }
        }
        public MainPageViewModel(string user, string pass)
        {
            AuthUser = user;
            AuthPass = pass;
            var lg = new AccessLogin().SelectAll();
            foreach (var item in lg)
            {
                if(item.Pass == AuthPass && item.User == AuthUser)
                {
                    AccessLevel = item.AccessLevel;
                }
            }
        }
    }
}
