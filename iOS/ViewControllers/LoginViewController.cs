// This file has been autogenerated from a class added in the UI designer.

using System;
using Foundation;
using UIKit;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    public partial class LoginViewController : UIViewController
    {
        public LoginViewController(IntPtr handle) : base(handle)
        {
        }
        public LoginViewController() : base("LoginViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            btnCreateUsername.TouchUpInside += (object sender, EventArgs e) =>
            {
                this.PerformSegue("LoginPerformed", sender as NSObject);
            };
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
            // Release any cached data, images, etc that aren't in use.
        }
    }
}