﻿using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.Essentials;
using System.Linq;
using System.Text.RegularExpressions;
using Foundation;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    public partial class CreateUsernameViewController : UIViewController
    {

        public UsersViewModel ViewModel { get; set; }

        public CreateUsernameViewController(IntPtr handle) : base(handle)
        {
        }
        public CreateUsernameViewController() : base("CreateUsernameViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnSaveUser.TouchUpInside += async (s, e) => {
                try
                {

                    var Username = txtEmail.Text;
                    var Password = txtPassword.Text;
                    await SecureStorage.SetAsync("Username", Username);
                    await SecureStorage.SetAsync("Password", Password);
                }
                catch (Exception ex)
                {
                    //TODO:add logger
                }
                //this.DismissModalViewController(true);
                var password = txtPassword.Text;
                var confirmPassword = txtPassword.Text;
                if ( isPasswordValid(password) && passwordsMatch(password, confirmPassword) )
                {
                    var user = new User
                    {
                        Username = txtEmail.Text,
                        //Description = txtDesc.Text
                    };
                    ViewModel.AddUserCommand.Execute(user);

                    var sender = s as NSObject;
                    this.PerformSegue("CreateUsernameToUsers", sender);
                }
                else
                {
                    displayAlert("Error", "Please correct the errors");
                }

            };

            txtPassword.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                validatePassword(textField.Text);
            }, UIControlEvent.EditingChanged);
        }
        private void displayAlert(string title, string message)
        {

            //Create Alert
            var okAlertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

            //Add Action
            okAlertController.AddAction(UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));

            // Present Alert
            PresentViewController(okAlertController, true, null);
        }
        private bool passwordsMatch(string password, string confirmPassword)
        {
            if (password == confirmPassword)
                return true;
            else
                return false;
        }
        private bool isPasswordValid(string password)
        {
            var ErrorsList = getPasswordErrors(password);
            if (ErrorsList.Count > 0)
                return false;
            else
                return true;
        }
        private void validatePassword(string password)
        {
            var ErrorsList = getPasswordErrors(password);
            if (ErrorsList.Count > 0)
            {
                txtPassword.TextColor = UIColor.Red;
                lblPasswordError.Text = ErrorsList.First();
                //lblPasswordError.Text = String.Join("" + Environment.NewLine, ErrorsList);
            }
            else
            {
                txtPassword.TextColor = UIColor.Black;
                lblPasswordError.Text = "";
            }
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

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

