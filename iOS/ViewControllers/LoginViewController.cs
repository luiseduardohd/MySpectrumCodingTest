using System;
using MySpectrumCodingTest.ViewModels;
using UIKit;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    public partial class LoginViewController : BaseViewController
    {
        public LoginViewModel LoginViewModel { get; set; }


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
            LoginViewModel = new LoginViewModel(completeAction);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            LinkEvents();
        }

        public void LinkEvents()
        {
            this.Initialize();

            Bind( txtUsername,   (x) => LoginViewModel.Username = x, LoginViewModel.Username);
            Bind( txtPassword,   (x) => LoginViewModel.Password = x, LoginViewModel.Password);
            Bind( btnSignIn,     LoginViewModel.LoginCommand);
            Bind( btnTroubleSigningIn, LoginViewModel.TroubleSigningInCommand);

            txtPassword.ShouldReturn = PasswordShouldReturn;

            TapToHideKeyboard(View);
        }

        private bool PasswordShouldReturn(UITextField textfield)
        {
            txtPassword.ResignFirstResponder();
            LoginViewModel.LoginCommand.Execute(null);
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