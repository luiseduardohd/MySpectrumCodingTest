using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using SQLite.Net.Async;
using SQLite;
using MySpectrumCodingTest.Extensions;
using System.Threading;

namespace MySpectrumCodingTest.Services
{
    public class SqliteDataStore : IDataStore<User>
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            var databasePath = Constants.DatabasePath;

            var SqliteAsyncConnection = new SQLiteAsyncConnection(databasePath);

            return SqliteAsyncConnection;
        });
        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        private bool initialized;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public SqliteDataStore()
        {
        }

        public async Task InitializeAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (!initialized)
                {
                    if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(User).Name))
                    {
                        _ = await Database.CreateTablesAsync(CreateFlags.None, typeof(User));
                        initialized = true;
                    }
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> AddAsync(User user)
        {
            await InitializeAsync();
            var result = await Database.InsertAsync(user);
            return true;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            await InitializeAsync();
            var result = await Database.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await InitializeAsync();
            User _user = await Database.Table<User>().Where((User arg) => arg.Id == id).FirstOrDefaultAsync();
            var result = await Database.DeleteAsync<User>(_user);
            return true;
        }

        public async Task<bool> DeleteAsync(User user)
        {
            await InitializeAsync();
            User _user = await Database.Table<User>().Where((User arg) => arg.Id == user.Id).FirstOrDefaultAsync();
            var result = await Database.DeleteAsync<User>(_user.Id);
            return true;
        }

        public async Task<User> GetAsync(int id)
        {
            await InitializeAsync();
            var result = await Database.GetAsync<User>(id);
            return result;
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool forceRefresh = false)
        {
            await InitializeAsync();
            var result = await Database.Table<User>().ToListAsync();
            return result;
        }
    }
}
