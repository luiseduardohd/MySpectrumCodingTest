using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.Essentials;
using System.Linq;
using System.Text.RegularExpressions;
using Foundation;
using MySpectrumCodingTest.ViewModels;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    public partial class UserViewController : BaseViewController
    {
        public UserViewModel UserViewModel { get; set; }

        public UserViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }
        public UserViewController() : base(nameof(UserViewController), null)
        {
            Initialize();
        }
        public void Initialize()
        {
            UserViewModel = new UserViewModel(this.CompleteAction, this.UseEmailErrors, this.UsePasswordErrors, this.UseConfirmPasswordErrors);
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnSaveUser.TouchUpInside += (s, e) =>
            {
                UserViewModel.SaveUserCommand.Execute(null);
            };
            txtUsername.Layer.BorderWidth = 1.0f;
            txtEmail.Layer.BorderWidth = 1.0f;
            txtPassword.Layer.BorderWidth = 1.0f;
            txtConfirmPassword.Layer.BorderWidth = 1.0f;

            txtUsername.Text = UserViewModel.Username;
            txtEmail.Text = UserViewModel.Email;
            txtPassword.Text = UserViewModel.Password;

            txtUsername.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                UserViewModel.Username = textField.Text;
            }, UIControlEvent.EditingChanged);
            txtEmail.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                UserViewModel.Email = textField.Text;
            }, UIControlEvent.EditingChanged);
            txtPassword.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                UserViewModel.Password = textField.Text;
            }, UIControlEvent.EditingChanged);
            txtConfirmPassword.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                UserViewModel.ConfirmPassword = textField.Text;
            }, UIControlEvent.EditingChanged);
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }



        private void UseEmailErrors(List<string> errors)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (errors.Count > 0)
                {
                    txtEmail.TextColor = UIColor.Red;
                    txtEmail.Layer.BorderColor = UIColor.Red.CGColor;
                }
                else
                {
                    txtEmail.TextColor = UIColor.Black;
                    txtEmail.Layer.BorderColor = UIColor.Green.CGColor;
                }
                lblEmailError.Text = errors.FirstOrDefault();
            });
        }
        private void UsePasswordErrors(List<string> errors)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (errors.Count > 0)
                {
                    txtPassword.TextColor = UIColor.Red;
                    txtPassword.Layer.BorderColor = UIColor.Red.CGColor;
                }
                else
                {
                    txtPassword.TextColor = UIColor.Black;
                    txtPassword.Layer.BorderColor = UIColor.Green.CGColor;
                }
                lblPasswordError.Text = errors.FirstOrDefault();
            });
        }
        private void UseConfirmPasswordErrors(List<string> errors)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (errors.Count > 0)
                {
                    txtConfirmPassword.TextColor = UIColor.Red;
                    txtConfirmPassword.Layer.BorderColor = UIColor.Red.CGColor;
                }
                else
                {
                    txtConfirmPassword.TextColor = UIColor.Black;
                    txtConfirmPassword.Layer.BorderColor = UIColor.Green.CGColor;
                }
                lblConfirmPasswordError.Text = errors.FirstOrDefault();
            });
        }

        private void CompleteAction(User user)
        {
            this.PerformSegue("CreateUsernameToUsers", this);
        }
    }
}

