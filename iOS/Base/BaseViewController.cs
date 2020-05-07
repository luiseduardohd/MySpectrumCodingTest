using System;
using CoreGraphics;
using Foundation;
using MySpectrumCodingTest.iOS.Extensions;
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
        protected void TapToHideKeyboard(UIView View)
        {
            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false;
            View.AddGestureRecognizer(g);
        }
        protected bool TextFieldShouldReturn(UITextField textField)
        {
            var nextTag = textField.Tag + 1;
            UIResponder nextResponder = this.View.ViewWithTag(nextTag);
            if (nextResponder != null)
            {
                nextResponder.BecomeFirstResponder();
            }
            else
            {
                textField.ResignFirstResponder();
            }
            return false;
        }
        protected void Bind(UITextField textField, Action<String> action, string text = null)
        {
            if (text != null)
                textField.Text = text;
            textField.AddTarget((s, e) => {
                UITextField tf = s as UITextField;
                action(tf.Text);
            }, UIControlEvent.EditingChanged);
            textField.ShouldReturn = TextFieldShouldReturn;
        }
        protected void Bind(UIButton button, bool hidden)
        {
            button.Hidden = hidden;
        }
        protected void Bind(UIButton btn, Command saveUserCommand)
        {
            btn.TouchUpInside += (s, e) => saveUserCommand.Execute(null);
        }

        protected virtual void RegisterForKeyboardNotifications()
        {
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
        }

        void OnKeyboardNotification(NSNotification notification)
        {
            bool visible = notification.Name == UIKeyboard.WillShowNotification;

            UIView.BeginAnimations("Keyboard");
            UIView.SetAnimationBeginsFromCurrentState(true);
            UIView.SetAnimationDuration(UIKeyboard.AnimationDurationFromNotification(notification));
            UIView.SetAnimationCurve((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification(notification));

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

            UIView.CommitAnimations();
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

            var relativeFrame = viewToCenter.Superview.ConvertRectToView(viewToCenter.Frame, scrollView);

            var landscape = this.InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft
                || this.InterfaceOrientation == UIInterfaceOrientation.LandscapeRight;

            var spaceAboveKeyboard = (landscape ? scrollView.Frame.Width : scrollView.Frame.Height) - keyboardHeight;

            var offset = relativeFrame.Y - (spaceAboveKeyboard - viewToCenter.Frame.Height) / 2;
            scrollView.ContentOffset = new CGPoint(0, offset);
        }

        protected virtual void RestoreScrollPosition(UIScrollView scrollView)
        {
            scrollView.ContentInset = UIEdgeInsets.Zero;
            scrollView.ScrollIndicatorInsets = UIEdgeInsets.Zero;
        }
    }
}
