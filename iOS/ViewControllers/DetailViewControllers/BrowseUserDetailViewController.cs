using System;

using UIKit;

namespace MySpectrumCodingTest.iOS.ViewControllers.DetailViewControllers
{
    public partial class BrowseUserDetailViewController : UIViewController
    {
        public UserDetailViewModel ViewModel { get; set; }
        public BrowseUserDetailViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = ViewModel.Title;
            ItemNameLabel.Text = ViewModel.User.Text;
            ItemDescriptionLabel.Text = ViewModel.User.Description;
        }
    }
}
