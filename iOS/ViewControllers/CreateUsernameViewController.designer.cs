// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    [Register ("CreateUsernameViewController")]
    partial class CreateUsernameViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSaveUser { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblConfirmPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblEmailError { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblPasswordError { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblUserNameError { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtConfirmPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPassword { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnSaveUser != null) {
                btnSaveUser.Dispose ();
                btnSaveUser = null;
            }

            if (lblConfirmPassword != null) {
                lblConfirmPassword.Dispose ();
                lblConfirmPassword = null;
            }

            if (lblEmailError != null) {
                lblEmailError.Dispose ();
                lblEmailError = null;
            }

            if (lblPasswordError != null) {
                lblPasswordError.Dispose ();
                lblPasswordError = null;
            }

            if (lblUserNameError != null) {
                lblUserNameError.Dispose ();
                lblUserNameError = null;
            }

            if (txtConfirmPassword != null) {
                txtConfirmPassword.Dispose ();
                txtConfirmPassword = null;
            }

            if (txtEmail != null) {
                txtEmail.Dispose ();
                txtEmail = null;
            }

            if (txtPassword != null) {
                txtPassword.Dispose ();
                txtPassword = null;
            }
        }
    }
}