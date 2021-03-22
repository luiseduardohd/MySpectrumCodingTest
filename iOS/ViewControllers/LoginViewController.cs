using System;
using MySpectrumCodingTest.ViewModels;
using UIKit;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    public partial class LoginViewController : BaseViewController
    {
        public override object ViewModel { get ; set ; }

        public LoginViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }
        public LoginViewController() : base(nameof(LoginViewController), null)
        {
            Initialize();
        }
        public void Initialize()
        {
            ViewModel = new LoginViewModel(completeAction);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            LinkEvents();
        }

        public void LinkEvents()
        {
            this.Initialize();
            var viewModel = ViewModel as LoginViewModel;
            Bind( txtUsername,   (x) => viewModel.Username = x, viewModel.Username);
            Bind( txtPassword,   (x) => viewModel.Password = x, viewModel.Password);
            Bind( btnSignIn, viewModel.LoginCommand);
            Bind(btnSignIn, nameof(btnSignIn.Enabled), nameof(viewModel.CanLogin));
            Bind( btnTroubleSigningIn, viewModel.TroubleSigningInCommand);

            txtPassword.ShouldReturn = PasswordShouldReturn;

            TapToHideKeyboard(View);
        }

        private bool PasswordShouldReturn(UITextField textfield)
        {
            txtPassword.ResignFirstResponder();
            (ViewModel as LoginViewModel).LoginCommand.Execute(null);
            return true;

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }


        private void completeAction()
        {
            this.PerformSegue("LoginPerformed", this);
        }
    }
}