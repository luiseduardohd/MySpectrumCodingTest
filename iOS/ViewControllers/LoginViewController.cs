// This file has been autogenerated from a class added in the UI designer.

using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MySpectrumCodingTest.iOS.Security;
using MySpectrumCodingTest.ViewModels;
//using PropertyChanged;
using UIKit;
using Xamarin.Essentials;
using iOSSecurity = Security;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
#if MVVMCROSS
    [MvxFromStoryboard("Main")]
    [MvxPagePresentation(WrapInNavigationController = false)]
#endif
    public partial class LoginViewController :
#if MVVMCROSS
        MvxViewController<LoginViewModel>
#else
    BaseViewController
    //<LoginViewModel>    
#endif
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
        private void completeAction()
        {
            this.PerformSegue("LoginPerformed", this);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

#if MVVMCROSS
            CreateBindings();
#else
            linkEvents();
#endif
        }
#if MVVMCROSS
        public void CreateBindings()
        {
            using (var set = this.CreateBindingSet<LoginViewController, LoginViewModel>())
            {
                set.Bind(btnSignIn).To(vm => vm.SignInCommand);
                set.Bind(btnTroubleSigningIn).To(vm => vm.TroubleSigningInCommand);
            }
        }
#endif

        public async void linkEvents()
        {
            try
            {
                await SecureStorage.SetAsync("Username", "luiseduardohd");
                await SecureStorage.SetAsync("Password", "Password1xyz");
            }
            catch (Exception ex)
            {

            }
            var Username = "";
            var Password = "";
            try
            {
                Username = await SecureStorage.GetAsync("Username");
                Password = await SecureStorage.GetAsync("Password");
            }
            catch (Exception ex)
            {
                
            }

            txtUsername.Text = Username;
            txtPassword.Text = Password;


            txtUsername.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                LoginViewModel.Password = textField.Text;
            }, UIControlEvent.EditingChanged);

            txtPassword.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                LoginViewModel.Password =  textField.Text;
            }, UIControlEvent.EditingChanged);


            btnSignIn.TouchUpInside += (object sender, EventArgs e) =>
            {
                LoginViewModel.LoginCommand.Execute(null);
            };
            btnTroubleSigningIn.TouchUpInside += (object sender, EventArgs e) =>
            {
                //TODO: validation
                //TODO: user can login with username, accounNumber and email.

                //this.PerformSegue("LoginPerformed", sender as NSObject);
            };
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}