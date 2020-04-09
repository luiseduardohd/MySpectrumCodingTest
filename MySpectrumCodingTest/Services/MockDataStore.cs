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
                //Test Data
            };

            foreach (User user in _users)
            {
                users.Add(user);
            }
        }

        public async Task<bool> AddAsync(User user)
        {
            var nextId = users.Select(t => t.Id).DefaultIfEmpty().Max( id => id ) + 1;
            user.Id = nextId;
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

        public async Task<bool> DeleteAsync(int id)
        {
            var _user = users.Where((User arg) => arg.Id == id).FirstOrDefault();
            users.Remove(_user);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(User user)
        {
            var _user = users.Where((User arg) => arg.Id == user.Id).FirstOrDefault();
            users.Remove(_user);

            return await Task.FromResult(true);
        }


        public async Task<User> GetAsync(int id)
        {
            return await Task.FromResult(users.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(users);
        }

    }
}
