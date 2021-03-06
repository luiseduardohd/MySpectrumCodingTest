﻿using System;
using MySpectrumCodingTest.ViewModels;
using UIKit;

namespace MySpectrumCodingTest.iOS.ViewControllers.DetailViewControllers
{
    public partial class BrowseUserViewController : UIViewController
    {
        public UserViewModel ViewModel { get; set; }
        public BrowseUserViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = ViewModel.Title;
            ItemNameLabel.Text = ViewModel.User.Username;
            ItemDescriptionLabel.Text = ViewModel.User.Email;
        }
    }
}
