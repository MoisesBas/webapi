using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace webapi.core.Interface
{
    
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(Int64 id);
        Task<List<T>> ListAllAsync();
        Task<List<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
