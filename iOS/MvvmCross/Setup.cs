using System;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.ViewModels;

namespace MySpectrumCodingTest.iOS.MvvmCross
{
    public class Setup : MvxIosSetup
    {
        public Setup()
        {
        }

        protected override IMvxApplication CreateApp()
        {
            //throw new NotImplementedException();
            return new App();
        }
    }
}
