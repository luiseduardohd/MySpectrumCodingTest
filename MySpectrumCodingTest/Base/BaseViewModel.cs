using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MySpectrumCodingTest.Resources;

namespace MySpectrumCodingTest
{
    public abstract class BaseViewModel :INotifyPropertyChanged 
    {
        public IDataStore<User> UsersDataStore => ServiceLocator.Instance.Get<IDataStore<User>>()
            //?? new UsersInMemoryDataStore()
            ;

        #region UserDialogs 
        protected IUserDialogs Dialogs { get; } = UserDialogs.Instance;
        #endregion

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { OnPropertyChanged(nameof(IsBusy));}
        }

        string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { OnPropertyChanged(nameof(IsBusy)); }
        }
        #region INotifyPropertyChanged
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        #endregion


        /// <summary>
        /// Gets the internationalized string at the given <paramref name="index"/>, which is the key of the resource.
        /// </summary>
        /// <param name="index">Index key of the string from the resources of internationalized strings.</param>
        public string this[string index] => Strings.ResourceManager.GetString(index);
    }
}
