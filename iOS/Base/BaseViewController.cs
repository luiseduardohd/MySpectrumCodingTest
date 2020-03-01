using System;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
//using PropertyChanged;
using UIKit;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    //[DoNotNotify]
    public class BaseViewController : UIViewController
    {

        public BaseViewController(IntPtr handle) : base(handle)
        {
        }
        public BaseViewController(string nibName, Foundation.NSBundle bundle) : base(nibName, bundle)
        {
        }
    }
    public class BaseViewController

        <TViewModel> :
        //:
        UIViewController
        //<TViewModel> :
        //MvxViewController<TViewModel>
        //    where TViewModel :
        //        class, IMvxViewModel
    {
        //public ViewModel ViewModel { get; set; } 
        public BaseViewController(IntPtr handle) : base(handle)
        {
        }
        //public BaseViewController() : base(nameof(BaseViewController), null)
        //public BaseViewController() : base(nameof(BaseViewController<TViewModel>), null)
        //{
        //}
        public BaseViewController(string nibName,Foundation.NSBundle bundle) : base(nibName, bundle)
        {
        }
    }
}
