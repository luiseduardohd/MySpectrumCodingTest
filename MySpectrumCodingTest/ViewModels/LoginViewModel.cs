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
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanLogin));
            }
        }


        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        private bool _canLogin = false;
        public bool CanLogin
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(_username) && !String.IsNullOrWhiteSpace(_password))
                    return true;
                else
                    return false;
            }
        }

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
                using (this.Dialogs.Loading("Loading"))
                {
                    await Task.Delay(1000);
                }
                bool accountExist = await AccountExist();
                if (!accountExist)
                {
                    await Dialogs.AlertAsync("The account doesn’t exist");
                    return;
                }

                bool isValidPassword = await IsValidPassword();
                if ( !isValidPassword)
                {
                    await Dialogs.AlertAsync("The password is incorrect");
                    return;
                }
                Dialogs.Toast("Login Successful");
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
        private async Task<bool> IsValidPassword()
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
        private async Task<bool> AccountExist()
        {
            bool success = false;
            var users = await UsersDataStore.GetAllAsync(true);
            var user = users.FirstOrDefault(x =>
            (
                x.Username == Username
                ||
                x.Email == Username
            ));

            if (user == null)
                success = false;
            else
                success = true;

            return success;
        }
    }
}