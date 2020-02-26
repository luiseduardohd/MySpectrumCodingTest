using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySpectrumCodingTest
{
    public class UsersInMemoryDataStore : IDataStore<User>
    {
        List<User> users;

        public UsersInMemoryDataStore()
        {
            users = new List<User>();
            var _users = new List<User>
            {
            };

            foreach (User user in _users)
            {
                users.Add(user);
            }
        }

        public async Task<bool> AddAsync(User user)
        {
            users.Add(user);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var _user = users.Where((User arg) => arg.Id == user.Id).FirstOrDefault();
            users.Remove(_user);
            users.Add(user);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var _user = users.Where((User arg) => arg.Id == id).FirstOrDefault();
            users.Remove(_user);

            return await Task.FromResult(true);
        }

        public async Task<User> GetAsync(string id)
        {
            return await Task.FromResult(users.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(users);
        }

    }
}
