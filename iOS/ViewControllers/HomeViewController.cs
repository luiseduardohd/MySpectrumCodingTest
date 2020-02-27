using System;

using UIKit;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    public partial class HomeViewController : UIViewController
    {
        public HomeViewController() : base("HomeViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            //this.NavigationController.NavigationBar.Hidden = true;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

