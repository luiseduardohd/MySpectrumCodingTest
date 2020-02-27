using Foundation;
using MySpectrumCodingTest.iOS.ViewControllers.DetailViewControllers;
using System;
using System.Collections.Specialized;
using UIKit;

namespace MySpectrumCodingTest.iOS
{
    public partial class UsersViewController : UITableViewController
    {
        UIRefreshControl refreshControl;

        public UsersViewModel ViewModel { get; set; }

        public UsersViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ViewModel = new UsersViewModel();

            // Setup UITableView.
            refreshControl = new UIRefreshControl();
            refreshControl.ValueChanged += RefreshControl_ValueChanged;
            TableView.Add(refreshControl);
            TableView.Source = new ItemsDataSource(ViewModel);

            Title = ViewModel.Title;

            ViewModel.PropertyChanged += IsBusy_PropertyChanged;
            ViewModel.Users.CollectionChanged += Items_CollectionChanged;

            bbtnLogout.Clicked += (s, e) => {
                //this.DismissModalViewController(true);
                NSObject sender = s as NSObject;
                this.PerformSegue("UsersToLogin", s as NSObject);
            };
        }
       

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (ViewModel.Users.Count == 0)
                ViewModel.LoadUsersCommand.Execute(null);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "NavigateToItemDetailSegue")
            {
                var controller = segue.DestinationViewController as BrowseUserDetailViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);
                var item = ViewModel.Users[indexPath.Row];

                controller.ViewModel = new UserDetailViewModel(item);
            }
            else if (segue.Identifier == "NavigateToNewItemSegue")
            {
                var controller = segue.DestinationViewController as UserNewViewController;
                controller.ViewModel = ViewModel;
            }
        }
        //public override void PrepareForSegue2(UIStoryboardSegue segue, NSObject sender)
        //{
        //    base.PrepareForSegue(segue, sender);

        //    //Set up Destination View Controller
        //    if (segue.Identifier == "loginSegue")
        //    {
        //        //var itvc = (IncidentTVC)segue.DestinationViewController;
        //        //if (itvc != null)
        //        //{
        //        //    itvc.setUser(this, currentUser);

        //        //}
        //    }


        //}

        void RefreshControl_ValueChanged(object sender, EventArgs e)
        {
            if (!ViewModel.IsBusy && refreshControl.Refreshing)
                ViewModel.LoadUsersCommand.Execute(null);
        }

        void IsBusy_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var propertyName = e.PropertyName;
            switch (propertyName)
            {
                case nameof(ViewModel.IsBusy):
                    {
                        InvokeOnMainThread(() =>
                        {
                            if (ViewModel.IsBusy && !refreshControl.Refreshing)
                                refreshControl.BeginRefreshing();
                            else if (!ViewModel.IsBusy)
                                refreshControl.EndRefreshing();
                        });
                    }
                    break;
            }
        }

        void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InvokeOnMainThread(() => TableView.ReloadData());
        }

        
    }

    class ItemsDataSource : UITableViewSource
    {
        static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_CELL");

        UsersViewModel viewModel;

        public ItemsDataSource(UsersViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override nint RowsInSection(UITableView tableview, nint section) => viewModel.Users.Count;
        public override nint NumberOfSections(UITableView tableView) => 1;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_IDENTIFIER, indexPath);

            var item = viewModel.Users[indexPath.Row];
            cell.TextLabel.Text = $"User name: {item.Text}";
            cell.DetailTextLabel.Text = $"User details: {item.Description}";
            cell.LayoutMargins = UIEdgeInsets.Zero;

            return cell;
        }
    }
}
