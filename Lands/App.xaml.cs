using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Lands
{
	using Views;
    using Helpers;
    using ViewModels;
    using Services;
    using Models;
    public partial class App : Application
    {
        #region Properties
        public static NavigationPage Navigator
        {
            get;
            internal set;
        }
        public static MasterPage Master 
        {
            get;
            internal set;
        }
        #endregion
        #region Constructors
        public App()
		{
			InitializeComponent();

            if (string.IsNullOrEmpty(Settings.Token))
            {
                this.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                //var dataService = new DataService();
                var dataAccess = new DataAccess();
                //var user = dataService.First<UserLocal>(false);
                var user = dataAccess.GetUser();
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = Settings.Token;
                mainViewModel.TokenType = Settings.TokenType;
                mainViewModel.User = user;
                mainViewModel.Lands = new LandsViewModel();
                this.MainPage = new MasterPage();
            }
			
		}
		#endregion

		#region Methods
		protected override void OnStart()
		{

			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
		#endregion
    }
}
