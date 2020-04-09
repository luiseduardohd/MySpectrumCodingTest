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
            txtUsername.Text = LoginViewModel.Username;
            txtPassword.Text = LoginViewModel.Password;


            txtUsername.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                LoginViewModel.Username = textField.Text;
            }, UIControlEvent.EditingChanged);
            txtUsername.ShouldReturn = TextFieldShouldReturn;

            txtPassword.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                LoginViewModel.Password =  textField.Text;
            }, UIControlEvent.EditingChanged);
            txtPassword.ShouldReturn = delegate
            {
                txtUsername.ResignFirstResponder();
                LoginViewModel.LoginCommand.Execute(null);
                return true;
            };

            btnSignIn.TouchUpInside += (object sender, EventArgs e) =>
            {
                LoginViewModel.LoginCommand.Execute(null);
            };
            btnTroubleSigningIn.TouchUpInside += (object sender, EventArgs e) =>
            {
                LoginViewModel.TroubleSigningInCommand.Execute(null);
            };

            TapToHideKeyboard(View);
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