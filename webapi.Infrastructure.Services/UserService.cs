using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.core.Entities;
using webapi.core.Filter;
using webapi.core.Interface;
using webapi.Infrastructure.Services.Interface;
using System.Linq;

namespace webapi.Infrastructure.Services
{


    namespace ADMS.Core.Services
    {
        /// <summary>
        /// User Service Class
        /// </summary>
        /// <seealso cref="webapi.Infrastructure.Services.Interface.IUserService" />
        public class UserService : IUserService
        {
            /// <summary>
            /// The user repository asynchronous
            /// </summary>
            private readonly IRepositoryAsync<UserEntities> _userRepositoryAsync;
            /// <summary>
            /// The user repository
            /// </summary>
            private readonly IRepository<UserEntities> _userRepository;
            /// <summary>
            /// Initializes a new instance of the <see cref="UserService"/> class.
            /// </summary>
            /// <param name="userRepository">The user repository.</param>
            /// <param name="userRepositoryAsync">The user repository asynchronous.</param>
            public UserService(IRepository<UserEntities> userRepository, IRepositoryAsync<UserEntities> userRepositoryAsync)
            {
                _userRepository = userRepository;
                _userRepositoryAsync = userRepositoryAsync;
            }

            /// <summary>
            /// Creates the user.
            /// </summary>
            /// <param name="user">The user.</param>
            /// <returns></returns>
            public Task CreateUser(UserEntities user) => _userRepositoryAsync.AddAsync(user);

            /// <summary>
            /// Gets all.
            /// </summary>
            /// <returns></returns>
            public async Task<List<UserEntities>> GetAll()
            {
                var result = await _userRepositoryAsync.ListAllAsync();
                return result;               
            }

            /// <summary>
            /// Ges the user by user name passowrd asynchronous.
            /// </summary>
            /// <param name="username">The username.</param>
            /// <param name="password">The password.</param>
            /// <returns></returns>
            public async Task<UserEntities> GeUserByUserNamePassowrdAsync(string username, string password)
            {
                var user = new UserSpecification(username, password);
                var result = await _userRepositoryAsync.ListAsync(user);
                return result.SingleOrDefault();

            }
        }
    }

}
