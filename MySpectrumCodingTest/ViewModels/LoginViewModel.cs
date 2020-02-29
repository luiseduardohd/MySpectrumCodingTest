using System;

namespace MySpectrumCodingTest.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields

        private string _password;
        private string _userName;
        private string _companyCode;
        private string _deviceIdentifier;
        #endregion

        public LoginViewModel(Action completeAction)
        {
        }


        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                SetProperty(ref _userName, value);
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                SetProperty(ref _password, value);
            }
        }

        public string CompanyCode
        {
            get
            {
                return _companyCode;
            }

            set
            {
                SetProperty(ref _companyCode, value);
            }
        }

        public string DeviceIdentifier
        {
            get
            {
                return _deviceIdentifier;
            }

            set
            {
                SetProperty(ref _deviceIdentifier, value);
            }
        }
        
    }
}