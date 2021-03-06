﻿using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.core.Interface
{
    /// <summary>
    /// Interface for Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T GetById(Int64 id);
        /// <summary>
        /// Gets the single by spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        T GetSingleBySpec(ISpecification<T> spec);
        /// <summary>
        /// Lists all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> ListAll();
        /// <summary>
        /// Lists the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        IEnumerable<T> List(ISpecification<T> spec);
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T Add(T entity);
        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);
    }
}
