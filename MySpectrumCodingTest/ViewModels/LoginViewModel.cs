using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Essentials;

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
        public Command TroubleSigningInCommand { get; set; }

        #endregion

        #region Private
        public object completeAction { get; set; }
        #endregion



        public LoginViewModel(Action completeAction)
        {

            LoginCommand = new Command(async (o)=> {
                bool success = await IsValidLogin();
                if ( ! success )
                {
                    using (this.Dialogs.Loading("Loading"))
                    {
                        await Task.Delay(2000);
                    }
                    await Dialogs.AlertAsync("There was a problem authenticating the user. Please verify your credentials");
                    return;
                }

                using (this.Dialogs.Loading("Loading"))
                {
                    await Task.Delay(2000);
                }

                completeAction?.Invoke();
            });
            TroubleSigningInCommand = new Command(
                async () =>
                {
                    await Browser.OpenAsync("https://id.spectrum.net/recover", BrowserLaunchMode.SystemPreferred);
                }
            );            
        }
        private async Task<bool> IsValidLogin()
        {
            bool success = false;
            var users = await UsersDataStore.GetAllAsync(true);
            var user = users.FirstOrDefault(x =>
            (
                x.Username == Username
                ||
                x.Email == Username
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