using System.Windows.Input;

namespace MySpectrumCodingTest
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Plugin.Share.CrossShare.Current.OpenBrowser("https://www.spectrum.net/"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
