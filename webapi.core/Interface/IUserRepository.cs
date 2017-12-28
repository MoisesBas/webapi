using System;
using System.Collections.Generic;
using System.Text;
using webapi.core.Entities;

namespace webapi.core.Interface
{
    /// <summary>
    /// Interface for User Repository
    /// </summary>
    /// <seealso cref="webapi.core.Interface.IRepository{webapi.core.Entities.UserEntities}" />
    public interface IUserRepository:IRepository<UserEntities>
    {

    }
}
