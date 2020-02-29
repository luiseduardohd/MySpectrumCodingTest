using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UIKit;

namespace MySpectrumCodingTest.iOS
{
    public partial class UserNewViewController : UIViewController
    {
        public UsersViewModel ViewModel { get; set; }

        public UserNewViewController(IntPtr handle) : base(handle)
        {
        }

        private void validatePassword(string password)
        {
            var ErrorsList = getPasswordErrors(password);
            if ( ErrorsList.Count > 0 )
            {
                txtPassword.TextColor = UIColor.Red;
                lblErrors.Text = String.Join( "" + Environment.NewLine, ErrorsList );
            }
            else
            {
                txtPassword.TextColor = UIColor.Black;
                lblErrors.Text = "";
            }
        }
        private List<string> getPasswordErrors(string password)
        {
            List<string> Errors = new List<string>();
            Regex lettersAndNumericalDigitsOnlyRegex = new Regex(@"[a-zA-Z0-9]+");
            if ( ! lettersAndNumericalDigitsOnlyRegex.IsMatch(password) )
            {
                Errors.Add("String must consist of a mixture of letters and numerical digits only");
            }
            Regex atLeastOneLetterRegex = new Regex(@".*[a-zA-Z].*");
            Regex atLeastOneNumberRegex = new Regex(@".*[0-9].*");
            if ( ! ( atLeastOneLetterRegex.IsMatch(password) && atLeastOneNumberRegex.IsMatch(password) ) )
            {
                Errors.Add("String must contains at least one letter and one numerical digit");
            }
            if ( !(  5 <= password.Length && password.Length <= 12 ) )
            {
                Errors.Add("String must be between 5 and 12 characters in length.");
            }
            Regex repeatedSecuenceRegex = new Regex(@"(?<secuence>[a-zA-Z\d]+)\k<secuence>.*");
            if ( repeatedSecuenceRegex.IsMatch(password) )
            {
                Errors.Add("String must not contain any sequence of characters immediately followed by the same sequence");
            }
            return Errors;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            txtPassword.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                validatePassword(textField.Text);
            }, UIControlEvent.EditingChanged);

            btnSaveItem.TouchUpInside += (sender, e) =>
            {
                var user = new User
                {
                    Username = txtTitle.Text,
                    Description = txtDesc.Text
                };
                ViewModel.AddUserCommand.Execute(user);
                NavigationController.PopToRootViewController(true);
            };
        }
    }
}
