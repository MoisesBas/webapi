using System;
using System.Collections.Generic;
using System.Text;
using webapi.core.Entities;

namespace webapi.core.Filter
{
    /// <summary>
    /// User Filtering
    /// </summary>
    /// <seealso cref="webapi.core.Filter.BaseSpecification{webapi.core.Entities.UserEntities}" />
    public class UserSpecification:BaseSpecification<UserEntities>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSpecification"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public UserSpecification(Int64 id)
             : base(i => (i.id== id)) {
           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSpecification"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public UserSpecification(string username,string password) : base(x => (
        username.Equals(x.userName) && password.Equals(x.password)
        ))
        {

        }       
    }
}
