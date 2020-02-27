//using System;
//using System.Windows.Input;
//using System.Threading.Tasks;
//namespace MySpectrumCodingTest.ViewModels
//{
//	public class SettingsViewModel : BaseViewModel
//	{
//		private ICommand _cancelCommand;
//		private ICommand _logoutCommand;

//		public SettingsViewModel()
//		{
//		}

//		public ICommand CancelCommand
//		{
//			get
//			{
//				_cancelCommand = _cancelCommand ?? new MvxCommand(() =>
//				{
//					CloseModal(this);
//				});

//				return _cancelCommand;
//			}
//		}

//		public ICommand LogoutCommand
//		{
//			get
//			{
//				_logoutCommand = _logoutCommand ?? new MvxCommand(() =>
//				{
//					CloseModal(this);

//					var presenterInitializer = Mvx.Resolve<IPresenterInitializer>();
//					presenterInitializer.Logout();
//				});

//				return _logoutCommand;
//			}
//		}
//	}
//}