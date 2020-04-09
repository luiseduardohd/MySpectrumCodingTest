using MySpectrumCodingTest.Services;

namespace MySpectrumCodingTest
{
    public class App
    {
        
        public static async void Init()
        {
            //ServiceLocator.Instance.Register<IDataStore<User>, UsersInMemoryDataStore>();
            ServiceLocator.Instance.Register<IDataStore<User>, SqliteDataStore>();
        }
    }
}
