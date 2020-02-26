using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MySpectrumCodingTest
{
    public class UserDetailViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public User User { get; set; }
        public string Password { get; set; }
        private Action<List<string>> passwordErrors;
        public UserDetailViewModel(User user = null,Action<List<string>> passwordErrors = null)
        {
            if (user != null)
            {
                Title = user.Text;
                User = user;
            }
            this.passwordErrors = passwordErrors;
            this.PropertyChanged += UserDetailViewModel_PropertyChanged;
        }
        void UserDetailViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            System.Console.WriteLine("Property " + e.PropertyName + " changed");
            switch (e.PropertyName)
            {
                case "Password":
                    if(passwordErrors!=null)
                    {
                        List<string> Errors= new List<string>();
                        //Regex rx = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b");
                        Regex lettersAndNumericalDigitsOnlyRegex = new Regex(@"[a-zA-Z0-9]+");
                        if (lettersAndNumericalDigitsOnlyRegex.IsMatch(Password)) {

                            Errors.Add("String must consist of a mixture of letters and numerical digits only");
                        }
                        Regex atLeastOneLetterRegex = new Regex(@".*[a-zA-Z].*");
                        Regex atLeastOneNumberRegex = new Regex(@".*[0-9].*");
                        if (atLeastOneLetterRegex.IsMatch(Password) && atLeastOneNumberRegex.IsMatch(Password))
                        {

                            Errors.Add("String must contains at least one letter and one numerical digit");
                        }
                        if ( 5 <= Password.Length && Password.Length <= 12)
                        {

                            Errors.Add("String must contains at least one letter and one numerical digit");
                        }
                        //Regex threeCharactersRepeatedRegex = new Regex(@"(...)\1{2}");
                        //Regex patternRepeatedRegex = new Regex(@"^(?!.*(?<g>[a-zA-Z\d]+)\k<g>.*)$");
                        Regex patternRepeatedRegex = new Regex(@"(?<pattern>[a-zA-Z\d]+)\k<pattern>.*");
                        if (patternRepeatedRegex.IsMatch(Password))
                        {
                            Errors.Add("String must consist of a mixture of letters and numerical digits only");
                        }
                        passwordErrors(Errors);
                    }
                    break;
            }
        }

    }
}
