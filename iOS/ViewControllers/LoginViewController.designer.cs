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
        UIKit.UIButton CreateAUsername { get; set; }


        [Outlet]
        UIKit.UITextField Password { get; set; }


        [Outlet]
        UIKit.UIButton SignIn { get; set; }


        [Outlet]
        UIKit.UIButton TroubleSignIn { get; set; }


        [Outlet]
        UIKit.UITextField Username { get; set; }

        [Action ("SignIn_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SignIn_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton189_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton189_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
        }
    }
}