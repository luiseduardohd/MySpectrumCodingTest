// This file has been autogenerated from a class added in the UI designer.

using System;
using Foundation;
using MySpectrumCodingTest.iOS.Security;
using UIKit;
using Xamarin.Essentials;
using iOSSecurity = Security;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    public partial class LoginViewController : UIViewController
    {
        public LoginViewController(IntPtr handle) : base(handle)
        {
        }
        public LoginViewController() : base(nameof(LoginViewController), null)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            //if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            //{
            //    var navBar = NavigationController.NavigationBar;
            //    navBar.LeadingAnchor.ConstraintEqualTo( View.LeadingAnchor).Active = true;
            //    navBar.TrailingAnchor.ConstraintEqualTo( View.TrailingAnchor).Active = true;
            //    navBar.TopAnchor.ConstraintEqualTo( View.SafeAreaLayoutGuide.TopAnchor).Active = true;
            //    navBar.HeightAnchor.ConstraintEqualTo(64).Active = true;
            //}
            //if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            //{
            //    if ( this.NavigationController != null )
            //    {
            //        var navBar = this.NavigationController;
            //        navBar.NavigationBar.PrefersLargeTitles = true;
            //    }
            //}
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

            
            btnSignIn.TouchUpInside += (object sender, EventArgs e) =>
            {
                this.PerformSegue("LoginPerformed", sender as NSObject);
            };
            btnTroubleSigningIn.TouchUpInside += (object sender, EventArgs e) =>
            {
                this.PerformSegue("LoginPerformed", sender as NSObject);
            };
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}