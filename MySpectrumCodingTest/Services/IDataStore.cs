using System.Collections.Generic;
using System.Threading.Tasks;

namespace MySpectrumCodingTest
{
    public interface IDataStore<T>
    {
        Task InitializeAsync();

        Task<bool> AddAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(T item);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(bool forceRefresh = false);
    }
}
