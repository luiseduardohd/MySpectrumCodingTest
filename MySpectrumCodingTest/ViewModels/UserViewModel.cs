using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MySpectrumCodingTest.Extensions;

namespace MySpectrumCodingTest.ViewModels
{
    public class UserViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";

        public Command SaveUserCommand { get; private set; }
        public Command DeleteUserCommand { get; private set; }

        public ObservableCollection<string> EmailErrors { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> PasswordErrors { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ConfirmPasswordErrors { get; set; } = new ObservableCollection<string>();
        public User User { get;  set; }

        public bool IsDeleteEnabled => User != null;

        private Action<User> userSaved;
        private Action<List<string>> useEmailErrors;
        private Action<List<string>> usePasswordErrors;
        private Action<List<string>> useConfirmPasswordErrors;
        


        public UserViewModel()
        {
            SaveUserCommand = new Command(() => SaveUser());
            this.PropertyChanged += async (object sender, PropertyChangedEventArgs e)
                => await UserDetailViewModel_PropertyChangedAsync(sender, e);
            DeleteUserCommand = new Command(() => DeleteUser());
            this.PropertyChanged += async (object sender, PropertyChangedEventArgs e)
                => await UserDetailViewModel_PropertyChangedAsync(sender, e);
        }

        public UserViewModel(User user):this()
        {
            this.User = user;
            this.Username = user.Username;
            this.Email = user.Email;
            this.Password = user.Password;
            this.ConfirmPassword = user.Password;
        }

        public UserViewModel(Action<User> userSaved, Action<List<string>> useEmailErrors = null, Action<List<string>> usePasswordErrors = null, Action<List<string>> useConfirmPasswordErrors = null)
        {
            Initialize(userSaved, useEmailErrors, usePasswordErrors, useConfirmPasswordErrors);
        }
        public void Initialize(Action<User> userSaved, Action<List<string>> useEmailErrors = null, Action<List<string>> usePasswordErrors = null, Action<List<string>> useConfirmPasswordErrors = null)
        {
            this.userSaved = userSaved;
            this.useEmailErrors = useEmailErrors;
            this.usePasswordErrors = usePasswordErrors;
            this.useConfirmPasswordErrors = useConfirmPasswordErrors;
        }

        private async void SaveUser()
        {
            var isValidPassword = IsValidPassword();
            var isValidEmail = IsValidEmail();

            if (isValidPassword && isValidEmail )
            {                
                if( this.User ==null )
                {
                    var user = new User()
                    {
                        Username = Username,
                        Email = Email,
                        Password = Password,
                    };
                    await UsersDataStore.AddAsync(user);
                    userSaved?.Invoke(user);

                    await Task.Delay(2000);
                    Dialogs.Toast("Welcome " + user.Username);
                }
                else
                {
                    var user = this.User;
                    user.Username = Username;
                    user.Email = Email;
                    user.Password = Password;
                    await UsersDataStore.UpdateAsync(user);
                    userSaved?.Invoke(user);

                    await Task.Delay(1000);
                    Dialogs.Toast("Saved " + user.Username);
                }

            }
            return;
        }
        private async void DeleteUser()
        {
            if (this.User != null)
            {
                var userName = this.User.Username;
                await UsersDataStore.DeleteAsync(this.User);

                await Task.Delay(2000);
                Dialogs.Toast("Deleted user: " + userName);
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
                        var Errors = GetPasswordErrors(Password);
                        PasswordErrors.AddRange<string>(Errors);
                        usePasswordErrors?.Invoke(Errors);
                    }
                    break;
                case "ConfirmPassword":
                    {
                        ConfirmPasswordErrors.Clear();
                        var Errors = GetConfirmPasswordErrors(Password,ConfirmPassword);
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

            if ( this.User == null && await IsUsedEmail(email) )
            {
                Errors.Add("This email is already in use");
            }

            return Errors;
        }

        private List<string> GetPasswordErrors(string password)
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
        private List<string> GetConfirmPasswordErrors(string password,string confirmPassword)
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

        private async Task<bool> IsUsedEmail(string email)
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

        private bool IsValidPassword()
        {
            return PasswordErrors.Count == 0;
        }

    }
}
