using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.ViewModels;
using MySpectrumCodingTest.Resources;

namespace MySpectrumCodingTest
{
    public abstract class BaseViewModel : MvxViewModel
    {
        public IDataStore<User> UsersDataStore => ServiceLocator.Instance.Get<IDataStore<User>>() ?? new UsersInMemoryDataStore();

        #region UserDialogs 
        protected IUserDialogs Dialogs { get; } = UserDialogs.Instance;
        #endregion

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        //public bool IsBusy { get; set; } = false;

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        //public string Title { get; set; } = string.Empty;
        /*
        MvxNotifyTask IMvxViewModel.InitializeTask { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        */
        #region INotifyPropertyChanged
        /*
        public override event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        */
        #endregion


        /// <summary>
        /// Gets the internationalized string at the given <paramref name="index"/>, which is the key of the resource.
        /// </summary>
        /// <param name="index">Index key of the string from the resources of internationalized strings.</param>
        public string this[string index] => Strings.ResourceManager.GetString(index);
    }
    public abstract class BaseViewModel<TParameter, TResult> : MvxViewModel<TParameter, TResult>
        where TParameter : class
        where TResult : class
    {
        protected BaseViewModel()
        {
        }

        /// <summary>
        /// Gets the internationalized string at the given <paramref name="index"/>, which is the key of the resource.
        /// </summary>
        /// <param name="index">Index key of the string from the resources of internationalized strings.</param>
        public string this[string index] => Strings.ResourceManager.GetString(index);
    }
}
