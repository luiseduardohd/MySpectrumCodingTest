using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using System.Linq;
using Xamarin.Essentials;
using Plugin.Share;
using Plugin.Share.Abstractions;

namespace MySpectrumCodingTest.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        public string Username { get; set; }
        public string Password { get; set; }

        public ObservableCollection<User> Users { get; set; }
        public Command LoginCommand { get; private set; }

        public Command SignInCommand { get;  set; }
        public Command TroubleSigningInCommand { get;  set; }

        #endregion

        #region Private
        public object completeAction { get; set; }
        private Task Initialization { get;  set; }
        #endregion



        public LoginViewModel(Action completeAction)
        {

            LoginCommand = new Command(async (o)=> {
                bool success = await isValidLogin();
                if ( ! success )
                {
                    using (this.Dialogs.Loading("Loading"))
                    {
                        await Task.Delay(2000);
                    }
                    await Dialogs.AlertAsync("There was a problem authenticating the user. Please verify your credentials");
                    return;
                }

                try
                {
                    await SecureStorage.SetAsync("Username", Username);
                    await SecureStorage.SetAsync("Password", Password);
                }
                catch (Exception)
                {
                }

                using (this.Dialogs.Loading("Loading"))
                {
                    await Task.Delay(2000);
                }

                completeAction?.Invoke();
            });
            TroubleSigningInCommand = new Command(() => CrossShare.Current.OpenBrowser("https://id.spectrum.net/recover",new BrowserOptions() {SafariControlTintColor = new ShareColor(255,255,255), SafariBarTintColor = new ShareColor(62, 146, 241), UseSafariWebViewController=true}));
            Initialization = InitializeAsync();
        }
        private async Task InitializeAsync()
        {
            await getCredentials();
            return ;
        }
        private async Task getCredentials()
        {
            try
            {
                Username = await SecureStorage.GetAsync("Username");
                Password = await SecureStorage.GetAsync("Password");
            }
            catch (Exception ex)
            {

            }
            return;
        }
        private async Task<bool> isValidLogin()
        {
            bool success = false;
            var users = await UsersDataStore.GetAllAsync(true);
            var user = users.FirstOrDefault(x =>
            (
                x.Username == Username
                ||
                x.Email == Username
                //||
                //x.AccountNumber == Username 
            )
            && x.Password == Password);

            if (user == null)
                success = false;
            else
                success = true;

            return success;
        }
    }
}