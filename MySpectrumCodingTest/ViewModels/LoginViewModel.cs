using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MySpectrumCodingTest.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        public string UserName { get; set; }
        public string Password { get; set; }

        public ObservableCollection<User> Users { get; set; }
        public Command LoginCommand { get; private set; }

        #endregion

        public LoginViewModel(Action completeAction)
        {

        }

        private async Task<bool> Login()
        {
            bool success = false;


            return success;
        }
    }
}