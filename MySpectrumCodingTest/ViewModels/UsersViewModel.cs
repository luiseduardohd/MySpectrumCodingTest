using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MySpectrumCodingTest
{
    public class UsersViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        public Command LoadUsersCommand { get; set; }
        public Command AddUserCommand { get; set; }

        public UsersViewModel()
        {
            Title = "Users";
            Users = new ObservableCollection<User>();
            LoadUsersCommand = new Command(async () => await ExecuteLoadUsersCommand());
            AddUserCommand = new Command<User>(async (User user) => await AddUser(user));
        }

        async Task ExecuteLoadUsersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Users.Clear();
                var users = await UsersDataStore.GetAllAsync(true);
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task AddUser(User user)
        {
            Users.Add(user);
            await UsersDataStore.AddAsync(user);
        }
    }
}