// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MySpectrumCodingTest.iOS.ViewControllers
{
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCreateUsername { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSignIn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnTroubleSigningIn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtUsername { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCreateUsername != null) {
                btnCreateUsername.Dispose ();
                btnCreateUsername = null;
            }

            if (btnSignIn != null) {
                btnSignIn.Dispose ();
                btnSignIn = null;
            }

            if (btnTroubleSigningIn != null) {
                btnTroubleSigningIn.Dispose ();
                btnTroubleSigningIn = null;
            }

            if (txtPassword != null) {
                txtPassword.Dispose ();
                txtPassword = null;
            }

            if (txtUsername != null) {
                txtUsername.Dispose ();
                txtUsername = null;
            }
        }
    }
}