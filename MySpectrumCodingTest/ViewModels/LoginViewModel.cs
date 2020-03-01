using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using System.Linq;

namespace MySpectrumCodingTest.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        public string UserName { get; set; }
        public string Password { get; set; }

        public ObservableCollection<User> Users { get; set; }
        public Command LoginCommand { get; private set; }

        public object SignInCommand { get;  set; }
        public object TroubleSigningInCommand { get;  set; }

        #endregion

        #region Private
        public object completeAction { get; set; }
        #endregion



        public LoginViewModel(Action completeAction)
        {
            LoginCommand = new Command(async (o)=> {
                bool success = await isValidLogin();
                if ( ! success )
                {
                    await Dialogs.AlertAsync("There was a problem authenticating the user. Please verify your credentials");
                    return;
                }

                using (this.Dialogs.Loading("Loading"))
                {
                    await Task.Delay(2000);
                }

                completeAction?.Invoke();
            });
        }

        private async Task<bool> isValidLogin()
        {
            bool success = false;
            var users = await UsersDataStore.GetAllAsync(true);
            var user = users.FirstOrDefault(x =>
            (
                x.Username == UserName
                ||
                x.Email == UserName
                ||
                x.AccountNumber == UserName 
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