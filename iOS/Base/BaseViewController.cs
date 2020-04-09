using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using UIKit;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
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
            //UIView.BeginAnimations("AnimateForKeyboard");
            //UIView.SetAnimationBeginsFromCurrentState(true);
            //UIView.SetAnimationDuration(UIKeyboard.AnimationDurationFromNotification(notification));
            //UIView.SetAnimationCurve((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification(notification));

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
            //UIView.CommitAnimations();
        }
        protected virtual void OnKeyboardChanged(bool visible, nfloat keyboardHeight)
        {
            var activeView = this.View.FindFirstResponder();
            var scrollView = activeView?.FindSuperviewOfType(this.View, typeof(UIScrollView)) as UIScrollView;

            if (scrollView == null)
                return;

            if (!visible)
            {
                RestoreScrollPosition(scrollView);
            }
            else
            {
                CenterViewInScroll(activeView, scrollView, keyboardHeight);
            }

        }
        protected virtual void CenterViewInScroll(UIView viewToCenter, UIScrollView scrollView, nfloat keyboardHeight)
        {
            var contentInsets = new UIEdgeInsets(0.0f, 0.0f, keyboardHeight, 0.0f);
            scrollView.ContentInset = contentInsets;
            scrollView.ScrollIndicatorInsets = contentInsets;

            // Position of the active field relative isnside the scroll view
            var relativeFrame = viewToCenter.Superview.ConvertRectToView(viewToCenter.Frame, scrollView);

            var landscape = this.InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft
                || this.InterfaceOrientation == UIInterfaceOrientation.LandscapeRight;

            var spaceAboveKeyboard = (landscape ? scrollView.Frame.Width : scrollView.Frame.Height) - keyboardHeight;

            // Move the active field to the center of the available space
            var offset = relativeFrame.Y - (spaceAboveKeyboard - viewToCenter.Frame.Height) / 2;
            scrollView.ContentOffset = new CGPoint(0, offset);
        }

        protected virtual void RestoreScrollPosition(UIScrollView scrollView)
        {
            scrollView.ContentInset = UIEdgeInsets.Zero;
            scrollView.ScrollIndicatorInsets = UIEdgeInsets.Zero;
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
