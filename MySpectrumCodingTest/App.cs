using System;

namespace MySpectrumCodingTest
{
    public class App
    {
        public static void Initialize()
        {
            ServiceLocator.Instance.Register<IDataStore<User>, UsersInMemoryDataStore>();
        }
    }
}
