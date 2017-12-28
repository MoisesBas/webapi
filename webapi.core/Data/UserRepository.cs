using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.core.Interface;

namespace webapi.core.Data
{
    /// <summary>
    /// User Respository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="webapi.core.Interface.IRepository{T}" />
    /// <seealso cref="webapi.core.Interface.IRepositoryAsync{T}" />
    public class UserRepository<T> : IRepository<T> , IRepositoryAsync<T> where T : class
    {
        /// <summary>
        /// The database context
        /// </summary>
        protected readonly UserContext _dbContext;
        /// <summary>
        /// The disposed
        /// </summary>
        private Boolean Disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository{T}" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <exception cref="ArgumentNullException">dbContext</exception>
        public UserRepository(UserContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.DbSet = _dbContext.Set<T>();


        }
        /// <summary>
        /// Gets or sets the database set.
        /// </summary>
        /// <value>
        /// The database set.
        /// </value>
        protected DbSet<T> DbSet { get; set; }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T GetById(Int64 id) => this.DbSet.Find(id);

        /// <summary>
        /// Gets the single by spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public T GetSingleBySpec(ISpecification<T> spec) => this.List(spec).FirstOrDefault();
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(Int64 id) => await this.DbSet.FindAsync(id);
        /// <summary>
        /// Lists all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> ListAll() => this.DbSet.AsEnumerable();
        /// <summary>
        /// Lists all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> ListAllAsync() => await this.DbSet.ToListAsync();

        /// <summary>
        /// Lists the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<T> List(ISpecification<T> spec)
        {
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

           
            return secondaryResult
                            .Where(spec.Criteria)
                            .AsEnumerable();
        }
        /// <summary>
        /// Lists the asynchronous.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public async Task<List<T>> ListAsync(ISpecification<T> spec)
        {
           
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

           
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

           
            return await secondaryResult
                            .Where(spec.Criteria)
                            .ToListAsync();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entity</exception>
        public T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entity</exception>
        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">entity</exception>
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entity</exception>
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">entity</exception>
        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entity</exception>
        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        

    }
}


