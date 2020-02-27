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

namespace MySpectrumCodingTest.iOS
{
    [Register ("UsersViewController")]
    partial class UsersViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton bbtnAddUser { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem bbtnLogout { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (bbtnAddUser != null) {
                bbtnAddUser.Dispose ();
                bbtnAddUser = null;
            }

            if (bbtnLogout != null) {
                bbtnLogout.Dispose ();
                bbtnLogout = null;
            }
        }
    }
}