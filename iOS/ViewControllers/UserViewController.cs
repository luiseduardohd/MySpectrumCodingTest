﻿using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.Essentials;
using System.Linq;
using System.Text.RegularExpressions;
using Foundation;
using MySpectrumCodingTest.ViewModels;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    public partial class UserViewController : BaseViewController
    {
        public UserViewModel UserViewModel { get; set; }

        public UserViewController(IntPtr handle) : base(handle)
        {
            UserViewModel = new UserViewModel();
            Initialize();
        }
        public UserViewController() : base(nameof(UserViewController), null)
        {
            UserViewModel = new UserViewModel();
            Initialize();
        }
        public void Initialize()
        {
            UserViewModel.Initialize(this.CompleteAction, this.UseEmailErrors, this.UsePasswordErrors, this.UseConfirmPasswordErrors);
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnSaveUser.TouchUpInside += (s, e) =>
            {
                UserViewModel.SaveUserCommand.Execute(null);
            };
            txtUsername.Layer.BorderWidth = 1.0f;
            txtEmail.Layer.BorderWidth = 1.0f;
            txtPassword.Layer.BorderWidth = 1.0f;
            txtConfirmPassword.Layer.BorderWidth = 1.0f;

            txtUsername.Text = UserViewModel.Username;
            txtEmail.Text = UserViewModel.Email;
            txtPassword.Text = UserViewModel.Password;

            txtUsername.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                UserViewModel.Username = textField.Text;
            }, UIControlEvent.EditingChanged);
            //txtUsername.ShouldReturn = delegate{
            //    txtUsername.ResignFirstResponder();
            //    return true;
            //};
            txtEmail.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                UserViewModel.Email = textField.Text;
            }, UIControlEvent.EditingChanged);
            //txtEmail.ShouldReturn = delegate {
            //    txtEmail.ResignFirstResponder();
            //    return true;
            //};
            txtPassword.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                UserViewModel.Password = textField.Text;
            }, UIControlEvent.EditingChanged);
            //txtPassword.ShouldReturn = delegate {
            //    txtUsername.ResignFirstResponder();
            //    return true;
            //};
            txtConfirmPassword.AddTarget((s, e) => {
                UITextField textField = s as UITextField;
                UserViewModel.ConfirmPassword = textField.Text;
            }, UIControlEvent.EditingChanged);
            txtConfirmPassword.ShouldReturn = delegate {
                txtConfirmPassword.ResignFirstResponder();
                return true;
            };
            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false;
            View.AddGestureRecognizer(g);
            //RegisterForKeyboardNotifications();
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        protected override void OnKeyboardChanged(bool visible, nfloat height)
        {
            //We "center" the popup when the keyboard appears/disappears
            var frame = View.Frame;

            if (visible)
                frame.Y -= height / 2f;
            else
                frame.Y += height / 2f;
            View.Frame = frame;
        }
        //protected virtual void RegisterForKeyboardNotifications()
        //{
        //    NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
        //    NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
        //}
        //protected virtual UIView KeyboardGetActiveView()
        //{
        //    return View.FindFirstResponder();
        //}
        //protected virtual void OnKeyboardChanged(bool visible, float keyboardHeight)
        //{
        //    var activeView = ViewToCenterOnKeyboardShown ?? KeyboardGetActiveView();
        //    if (activeView == null)
        //        return;

        //    var scrollView = activeView.FindSuperviewOfType(View, typeof(UIScrollView)) as UIScrollView;
        //    if (scrollView == null)
        //        return;

        //    if (!visible)
        //        RestoreScrollPosition(scrollView);
        //    else
        //        CenterViewInScroll(activeView, scrollView, keyboardHeight);
        //}
        //private void OnKeyboardNotification(NSNotification notification)
        //{
        //    if (!IsViewLoaded) return;

        //    //Check if the keyboard is becoming visible
        //    var visible = notification.Name == UIKeyboard.WillShowNotification;

        //    //Start an animation, using values from the keyboard
        //    UIView.BeginAnimations("AnimateForKeyboard");
        //    UIView.SetAnimationBeginsFromCurrentState(true);
        //    UIView.SetAnimationDuration(UIKeyboard.AnimationDurationFromNotification(notification));
        //    UIView.SetAnimationCurve((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification(notification));

        //    //Pass the notification, calculating keyboard height, etc.
        //    bool landscape = InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight;
        //    var keyboardFrame = visible
        //                            ? UIKeyboard.FrameEndFromNotification(notification)
        //                            : UIKeyboard.FrameBeginFromNotification(notification);

        //    OnKeyboardChanged(visible, landscape ? keyboardFrame.Width : keyboardFrame.Height);

        //    //Commit the animation
        //    UIView.CommitAnimations();
        //}


        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "CreateUsernameToUsers")
            {
                var tabBarController = segue.DestinationViewController as UITabBarController;
                var navigationController = tabBarController.ChildViewControllers[0] as UINavigationController;
                var controller = navigationController.ChildViewControllers[0] as UsersViewController;
                controller.ViewModel.LoadUsersCommand?.Execute(null);
            }
        }


        private void UseEmailErrors(List<string> errors)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (errors.Count > 0)
                {
                    txtEmail.TextColor = UIColor.Red;
                    txtEmail.Layer.BorderColor = UIColor.Red.CGColor;
                }
                else
                {
                    txtEmail.TextColor = UIColor.Black;
                    txtEmail.Layer.BorderColor = UIColor.Green.CGColor;
                }
                lblEmailError.Text = errors.FirstOrDefault();
            });
        }
        private void UsePasswordErrors(List<string> errors)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (errors.Count > 0)
                {
                    txtPassword.TextColor = UIColor.Red;
                    txtPassword.Layer.BorderColor = UIColor.Red.CGColor;
                }
                else
                {
                    txtPassword.TextColor = UIColor.Black;
                    txtPassword.Layer.BorderColor = UIColor.Green.CGColor;
                }
                lblPasswordError.Text = errors.FirstOrDefault();
            });
        }
        private void UseConfirmPasswordErrors(List<string> errors)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (errors.Count > 0)
                {
                    txtConfirmPassword.TextColor = UIColor.Red;
                    txtConfirmPassword.Layer.BorderColor = UIColor.Red.CGColor;
                }
                else
                {
                    txtConfirmPassword.TextColor = UIColor.Black;
                    txtConfirmPassword.Layer.BorderColor = UIColor.Green.CGColor;
                }
                lblConfirmPasswordError.Text = errors.FirstOrDefault();
            });
        }

        private void CompleteAction(User user)
        {
            this.PerformSegue("CreateUsernameToUsers", this);
        }
    }
}

