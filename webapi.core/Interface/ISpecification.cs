using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace webapi.core.Interface
{
    /// <summary>
    /// Interface for Filtering
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Gets the criteria.
        /// </summary>
        /// <value>
        /// The criteria.
        /// </value>
        Expression<Func<T, bool>> Criteria { get; }
        /// <summary>
        /// Gets the includes.
        /// </summary>
        /// <value>
        /// The includes.
        /// </value>
        List<Expression<Func<T, object>>> Includes { get; }
        /// <summary>
        /// Gets the include strings.
        /// </summary>
        /// <value>
        /// The include strings.
        /// </value>
        List<string> IncludeStrings { get; }
    }
}
