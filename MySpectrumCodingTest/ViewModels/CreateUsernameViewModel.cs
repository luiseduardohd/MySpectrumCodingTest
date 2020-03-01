using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using MySpectrumCodingTest.Extensions;
using PropertyChanged;

namespace MySpectrumCodingTest.ViewModels
{
    public class CreateUsernameViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public Command SaveUserCommand { get; private set; }


        //public string Password =>
        
        public ObservableCollection<string> EmailErrors { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> PasswordErrors { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ConfirmPasswordErrors { get; set; } = new ObservableCollection<string>();

        private Action<User> userCreated;
        private Action<List<string>> useEmailErrors;
        private Action<List<string>> usePasswordErrors;
        private Action<List<string>> useConfirmPasswordErrors;


        //public CreateUsernameViewModel(User user = null, Action<List<string>> passwordErrors = null)
        public CreateUsernameViewModel(Action<User> userCreated, Action<List<string>> useEmailErrors = null,Action < List<string>> usePasswordErrors = null, Action<List<string>> useConfirmPasswordErrors = null)
        {
            //if (user != null)
            //{
            //    Title = user.Username;
            //    User = user;
            //}
            this.userCreated = userCreated;
            this.useEmailErrors = useEmailErrors;
            this.usePasswordErrors = usePasswordErrors;
            this.useConfirmPasswordErrors = useConfirmPasswordErrors;
            SaveUserCommand = new Command( () =>   SaveUser());
            this.PropertyChanged += async (object sender, PropertyChangedEventArgs e)
                => await UserDetailViewModel_PropertyChangedAsync( sender, e);
        }

        private async void SaveUser()
        {
            if( isValidPassword(Password) && isValidEmail() )
            {
                var user = new User() {
                    Password = Password,
                    Email = Email,
                };
                await UsersDataStore.AddAsync(user);
                userCreated?.Invoke(user);
                Dialogs.Toast("Welcome "+user.Username);
            }
            return;
        }

        async Task UserDetailViewModel_PropertyChangedAsync(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("Property " + e.PropertyName + " changed");
            switch (e.PropertyName)
            {
                case "Email":
                    { 
                        EmailErrors.Clear();
                        var Errors = await GetEmailErrors(Email);
                        EmailErrors.AddRange<string>(Errors);
                        useEmailErrors?.Invoke(Errors);
                    }
                    break;
                case "Password":
                    {
                        PasswordErrors.Clear();
                        var Errors = getPasswordErrors(Password);
                        PasswordErrors.AddRange<string>(Errors);
                        usePasswordErrors?.Invoke(Errors);
                    }
                    break;
                case "ConfirmPassword":
                    {
                        ConfirmPasswordErrors.Clear();
                        var Errors = getConfirmPasswordErrors(Password,ConfirmPassword);
                        ConfirmPasswordErrors.AddRange<string>(Errors);
                        useConfirmPasswordErrors?.Invoke(Errors);
                    }
                    break;
            }
        }
        private async Task<List<string>> GetEmailErrors(string email)
        {
            List<string> Errors = new List<string>();
            if (email == String.Empty)
            {
                Errors.Add("Email cannot be empty");
            }

            if (!email.IsEmail())
            {
                Errors.Add("Please use a valid email address format");
            }

            if ( await isUsedEmail(email) )
            {
                Errors.Add("This email is already in use");
            }

            return Errors;
        }

        private List<string> getPasswordErrors(string password)
        {
            List<string> Errors = new List<string>();
            Regex lettersAndNumericalDigitsOnlyRegex = new Regex(@"[a-zA-Z0-9]+");
            if (!lettersAndNumericalDigitsOnlyRegex.IsMatch(password))
            {
                Errors.Add("String must consist of a mixture of letters and numerical digits only");
            }
            Regex atLeastOneLetterRegex = new Regex(@".*[a-zA-Z].*");
            Regex atLeastOneNumberRegex = new Regex(@".*[0-9].*");
            if (!(atLeastOneLetterRegex.IsMatch(password) && atLeastOneNumberRegex.IsMatch(password)))
            {
                Errors.Add("String must contains at least one letter and one numerical digit");
            }
            if (!(5 <= password.Length && password.Length <= 12))
            {
                Errors.Add("String must be between 5 and 12 characters in length.");
            }
            Regex repeatedSecuenceRegex = new Regex(@"(?<secuence>[a-zA-Z\d]+)\k<secuence>.*");
            if (repeatedSecuenceRegex.IsMatch(password))
            {
                Errors.Add("String must not contain any sequence of characters immediately followed by the same sequence");
            }
            return Errors;
        }
        private List<string> getConfirmPasswordErrors(string password,string confirmPassword)
        {
            List<string> Errors = new List<string>();
            if ( password != String.Empty && confirmPassword == String.Empty )
            {
                Errors.Add("Please confirm password");
            }

            if ( password != confirmPassword )
            {
                Errors.Add("Password doesn't match");
            }
            return Errors;
        }

        private async Task<bool> isUsedEmail(string email)
        {
            var users =  await UsersDataStore.GetAllAsync();
            var user = users.FirstOrDefault(x => x.Email == email);

            if( user == null )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isValidEmail()
        {
            return EmailErrors.Count == 0;
        }

        private bool isValidPassword(string password)
        {
            return PasswordErrors.Count == 0;
        }

    }
}
