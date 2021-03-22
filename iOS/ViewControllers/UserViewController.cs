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
        public override object ViewModel { get; set; }

        public UserViewController(IntPtr handle) : base(handle)
        {
            ViewModel = new UserViewModel();
            Initialize();
        }
        public UserViewController() : base(nameof(UserViewController), null)
        {
            ViewModel = new UserViewModel();
            Initialize();
        }
        public void Initialize()
        {
            (ViewModel as UserViewModel).Initialize(this.CompleteAction, this.DeleteAction, this.UseEmailErrors, this.UsePasswordErrors, this.UseConfirmPasswordErrors);
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var viewModel = ViewModel as UserViewModel;
            Bind( txtUsername,   (x)=> viewModel.Username = x, viewModel.Username);
            Bind( txtEmail,      (x) => viewModel.Email = x, viewModel.Email);
            Bind( txtPassword,   (x) => viewModel.Password = x, viewModel.Password);
            Bind( txtConfirmPassword, (x) => viewModel.Password = x);
            Bind( btnSaveUser, viewModel.SaveUserCommand);
            Bind( btnDeleteUser, !viewModel.IsDeleteEnabled);
            Bind( btnDeleteUser, viewModel.DeleteUserCommand);

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
        private void DeleteAction()
        {
            this.PerformSegue("CreateUsernameToUsers", this);
        }
    }
}

