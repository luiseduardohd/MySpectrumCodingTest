using System;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Plugin;
using MvvmCross.ViewModels;

namespace MySpectrumCodingTest
{
    public class App
        :
        MvxApplication
    {
        
        public static void Initialize()
        {
            ServiceLocator.Instance.Register<IDataStore<User>, UsersInMemoryDataStore>();
        }
        
        //public IMvxViewModelLocator FindViewModelLocator(MvxViewModelRequest request)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void Initialize()
        //{
        //    //RegisterNavigationServiceAppStart<ViewModels.LoginViewModel>();
        //    //Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
        //}

        //public void LoadPlugins(IMvxPluginManager pluginManager)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Reset()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task Startup()
        //{
        //    throw new NotImplementedException();
        //}
        //protected override  IMvxTouchViewsContainer CreateTouchViewsContainer()
        //{
        //    return new MvxStoryboardTouchViewsContainer("Main");
        //}
    }
}
