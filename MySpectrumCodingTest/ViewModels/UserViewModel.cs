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
using Xamarin.Essentials;

namespace MySpectrumCodingTest.ViewModels
{
    public class UserViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public Command SaveUserCommand { get; private set; }

        public ObservableCollection<string> EmailErrors { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> PasswordErrors { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ConfirmPasswordErrors { get; set; } = new ObservableCollection<string>();
        public User User { get;  set; }

        private Action<User> userSaved;
        private Action<List<string>> useEmailErrors;
        private Action<List<string>> usePasswordErrors;
        private Action<List<string>> useConfirmPasswordErrors;
        

        public UserViewModel(Action<User> userSaved, Action<List<string>> useEmailErrors = null, Action<List<string>> usePasswordErrors = null, Action<List<string>> useConfirmPasswordErrors = null)
        {
            this.userSaved = userSaved;
            this.useEmailErrors = useEmailErrors;
            this.usePasswordErrors = usePasswordErrors;
            this.useConfirmPasswordErrors = useConfirmPasswordErrors;
            SaveUserCommand = new Command(() => SaveUser());
            this.PropertyChanged += async (object sender, PropertyChangedEventArgs e)
                => await UserDetailViewModel_PropertyChangedAsync(sender, e);
        }

        public UserViewModel(User user)
        {
            this.User = user;
            this.Username = user.Username;
            this.Email = user.Email;
            this.Password = user.Password;
            this.ConfirmPassword = user.Password;
            SaveUserCommand = new Command(() => SaveUser());
            this.PropertyChanged += async (object sender, PropertyChangedEventArgs e)
                => await UserDetailViewModel_PropertyChangedAsync(sender, e);
        }

        private async void SaveUser()
        {
            if( IsValidPassword(Password) && IsValidEmail() )
            {
                try
                {
                    await SecureStorage.SetAsync("Username", Username);
                    await SecureStorage.SetAsync("Password", Password);
                }
                catch (Exception)
                {
                }

                var users = await UsersDataStore.GetAllAsync();
                User user = null;
                if( this.User ==null )
                {
                    var nextId = users.Max(x => x.Id) + 1;
                    user = new User()
                    {
                        Id = nextId,
                        Username = Username,
                        Email = Email,
                        Password = Password,
                    };
                    await UsersDataStore.AddAsync(user);

                    await Task.Delay(2000);
                    Dialogs.Toast("Welcome " + user.Username);
                }
                else
                {
                    user = this.User;
                    await UsersDataStore.UpdateAsync(user);

                    await Task.Delay(1000);
                    Dialogs.Toast("Saved " + user.Username);
                }
                userSaved?.Invoke(user);

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

        private bool IsValidEmail()
        {
            return EmailErrors.Count == 0;
        }

        private bool IsValidPassword(string password)
        {
            return PasswordErrors.Count == 0;
        }

    }
}
