using System;

namespace Lands.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Models;
    using Helpers;
    using Lands.Domain;

    public class MainViewModel
    {
        #region Properties
        public List<Land> LandsList
        {
            get;
            set;
        }
        public ObservableCollection<MenuItemViewModel> Menus
        {
            get;
            set;
        }
        public UserLocal User
        {
            get;
            set;
        }
        #endregion

        public string Token
        {
            get;
            set;
        }
        public string TokenType
        {
            get;
            set;
        }

        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }
        public LandsViewModel Lands
        {
            get;
            set;
        }
        public LandViewModel Land
        {
            get;
            set;
        }
        public RegisterViewModel Register
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.LoadMenu();
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_settingsg",
                PageName = "MyProfilePage",
                Title = Languages.MyProfile,
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_charg",
                PageName = "StaticsPage",
                Title = Languages.Statics,
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_exitg",
                PageName = "LoginPage",
                Title = Languages.LogOut,
            });
        }
        #endregion

        #region Singleton1
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}

