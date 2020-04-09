using System;
using System.Collections.Generic;
using UIKit;
using System.Linq;
using Foundation;
using MySpectrumCodingTest.ViewModels;
using System.Threading.Tasks;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    public partial class UserViewController : BaseViewController
    {
        public UserViewModel UserViewModel { get; set; }

        public UserViewController(IntPtr handle) : base(handle)
        {
            UserViewModel = new UserViewModel();
            Initialize();
        }
        public UserViewController() : base(nameof(UserViewController), null)
        {
            UserViewModel = new UserViewModel();
            Initialize();
        }
        public void Initialize()
        {
            UserViewModel.Initialize(this.CompleteAction, this.UseEmailErrors, this.UsePasswordErrors, this.UseConfirmPasswordErrors);
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            txtUsername.Layer.BorderWidth = 1.0f;
            txtEmail.Layer.BorderWidth = 1.0f;
            txtPassword.Layer.BorderWidth = 1.0f;
            txtConfirmPassword.Layer.BorderWidth = 1.0f;

            txtUsername.Text = UserViewModel.Username;
            txtEmail.Text = UserViewModel.Email;
            txtPassword.Text = UserViewModel.Password;

            btnDeleteUser.Hidden = ! UserViewModel.IsDeleteEnabled;

            txtUsername.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                UserViewModel.Username = textField.Text;
            }, UIControlEvent.EditingChanged);
            txtUsername.ShouldReturn = TextFieldShouldReturn;

            txtEmail.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                UserViewModel.Email = textField.Text;
            }, UIControlEvent.EditingChanged);
            txtEmail.ShouldReturn = TextFieldShouldReturn;

            txtPassword.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                UserViewModel.Password = textField.Text;
            }, UIControlEvent.EditingChanged);
            txtPassword.ShouldReturn = TextFieldShouldReturn;

            txtConfirmPassword.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                UserViewModel.ConfirmPassword = textField.Text;
            }, UIControlEvent.EditingChanged);
            txtConfirmPassword.ShouldReturn = TextFieldShouldReturn;

            btnSaveUser.TouchUpInside += async (s, e) =>
            {
                UserViewModel.SaveUserCommand.Execute(null);
            };
            btnDeleteUser.TouchUpInside += async (s, e) =>
            {
                UserViewModel.DeleteUserCommand.Execute(null);
            };

        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }


        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "CreateUsernameToUsers")
            {
                var tabBarController = segue.DestinationViewController as UITabBarController;
                var navigationController = tabBarController.ChildViewControllers[0] as UINavigationController;
                var controller = navigationController.ChildViewControllers[0] as UsersViewController;
                controller.ViewModel.LoadUsersCommand?.Execute(null);
            }
        }


        private void UseEmailErrors(List<string> errors)
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
        }
        private void UsePasswordErrors(List<string> errors)
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
        }
        private void UseConfirmPasswordErrors(List<string> errors)
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
        }

        private void CompleteAction(User user)
        {
            this.PerformSegue("CreateUsernameToUsers", this);
        }
    }
}

