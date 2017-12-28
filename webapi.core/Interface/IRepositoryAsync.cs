using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace webapi.core.Interface
{

    /// <summary>
    /// Interface Repository for Async
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryAsync<T> where T : class
    {
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Int64 id);
        /// <summary>
        /// Lists all asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<T>> ListAllAsync();
        /// <summary>
        /// Lists the asynchronous.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        Task<List<T>> ListAsync(ISpecification<T> spec);
        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);
        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task UpdateAsync(T entity);
        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task DeleteAsync(T entity);
    }
}
