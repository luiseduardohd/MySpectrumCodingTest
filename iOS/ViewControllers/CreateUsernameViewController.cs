using System;

using UIKit;
using Xamarin.Essentials;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    public partial class CreateUsernameViewController : UIViewController
    {
        public CreateUsernameViewController(IntPtr handle) : base(handle)
        {
        }
        public CreateUsernameViewController() : base("CreateUsernameViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            bbtnCancel.Clicked += (s, e) => {
                this.DismissModalViewController(true);
            };
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
                }
                this.DismissModalViewController(true);
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

