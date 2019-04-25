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
    using System.Threading.Tasks;

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


            if (Settings.IsRemembered == "true")
            {
                var dataAccess = new DataAccess();
                var token = dataAccess.GetToken();

                if(token != null & token.Expires > DateTime.Now)
                {
                    var user = dataAccess.GetUser();
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token = token;
                    mainViewModel.User = user;
                    mainViewModel.Lands = new LandsViewModel();
                    Application.Current.MainPage = new MasterPage();
                }
                else
                {
                    this.MainPage = new NavigationPage(new LoginPage());
                }

            }
            else
            {
                this.MainPage = new NavigationPage(new LoginPage());
            }
			
		}
        #endregion

        #region Methods
        public static Action HideLoginView
        {
            get
            {
                return new Action(() => Application.Current.MainPage =
                                  new NavigationPage(new LoginPage()));
            }
        }

        public static async Task NavigateToProfile(FacebookResponse profile)
        {
            if (profile == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var apiService = new ApiService();
            var dataService = new DataService();
            var dataAccess = new DataAccess();

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var token = await apiService.LoginFacebook(
                apiSecurity,
                "/api",
                "/Users/LoginFacebook",
                profile);

            if (token == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var user = await apiService.GetUserByEmail(
                apiSecurity,
                "/api",
                "/Users/GetUserByEmail",
                token.UserName,
                token.TokenType,
                token.AccessToken
                );

            UserLocal userLocal = null;
            if (user != null)
            {
                userLocal = Converter.ToUserLocal(user);
                dataAccess.InsertUser(userLocal);
                dataAccess.InsertToken(token);
                //dataService.DeleteAllAndInsert(userLocal);
                //dataService.DeleteAllAndInsert(token);
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.User = userLocal;
            mainViewModel.Lands = new LandsViewModel();
            Application.Current.MainPage = new MasterPage();
            Settings.IsRemembered = "true";

            mainViewModel.Lands = new LandsViewModel();
            Application.Current.MainPage = new MasterPage();
        }

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
