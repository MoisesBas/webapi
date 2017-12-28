using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.core.Entities;

namespace webapi.Infrastructure.Services.Interface
{
    /// <summary>
    /// Interface for User Service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        Task<List<UserEntities>> GetAll();
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task CreateUser(UserEntities user);
        /// <summary>
        /// Ges the user by user name passowrd asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<UserEntities> GeUserByUserNamePassowrdAsync(string username, string password);
       
    }
}
