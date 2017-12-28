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
        public class UserService : IUserService
        {
            private readonly IRepositoryAsync<UserEntities> _userRepositoryAsync;
            private readonly IRepository<UserEntities> _userRepository;
            public UserService(IRepository<UserEntities> userRepository, IRepositoryAsync<UserEntities> userRepositoryAsync)
            {
                _userRepository = userRepository;
                _userRepositoryAsync = userRepositoryAsync;
            }

            public Task CreateUser(UserEntities user) => _userRepositoryAsync.AddAsync(user);

            public async Task<List<UserEntities>> GetAll()
            {
                var result = await _userRepositoryAsync.ListAllAsync();
                return result;               
            }

            public async Task<UserEntities> GeUserByUserNamePassowrdAsync(string username, string password)
            {
                var user = new UserSpecification(username, password);
                var result = await _userRepositoryAsync.ListAsync(user);
                return result.SingleOrDefault();

            }
        }
    }

}
