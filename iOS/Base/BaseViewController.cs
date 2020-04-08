using System;
using Foundation;
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

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            RegisterForKeyboardNotifications();
        }

        protected virtual void RegisterForKeyboardNotifications()
        {
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
        }

        void OnKeyboardNotification(NSNotification notification)
        {
            //Check if the keyboard is becoming visible
            bool visible = notification.Name == UIKeyboard.WillShowNotification;

            //Start an animation, using values from the keyboard
            UIView.BeginAnimations("AnimateForKeyboard");
            UIView.SetAnimationBeginsFromCurrentState(true);
            UIView.SetAnimationDuration(UIKeyboard.AnimationDurationFromNotification(notification));
            UIView.SetAnimationCurve((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification(notification));

            //Pass the notification, calculating keyboard height, etc.
            bool landscape = InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight;
            if (visible)
            {
                var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
                OnKeyboardChanged(visible, landscape ? keyboardFrame.Width : keyboardFrame.Height);
            }
            else
            {
                var keyboardFrame = UIKeyboard.FrameBeginFromNotification(notification);
                OnKeyboardChanged(visible, landscape ? keyboardFrame.Width : keyboardFrame.Height);
            }

            //Commit the animation
            UIView.CommitAnimations();
        }
        protected virtual void OnKeyboardChanged(bool visible, nfloat height)
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
