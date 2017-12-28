using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webapi.core.Entities;

namespace webapi.Infrastructure.Services.Interface
{
   public interface IUserService
    {
        Task<List<UserEntities>> GetAll();
       Task CreateUser(UserEntities user);
       Task<UserEntities> GeUserByUserNamePassowrdAsync(string username, string password);
       
    }
}
