using System;
using System.Collections.Generic;
using System.Text;
using webapi.core.Entities;

namespace webapi.core.Filter
{
   public class UserSpecification:BaseSpecification<UserEntities>
    {
        public UserSpecification(Int64 id)
             : base(i => (i.id== id)) {
           
        }

        public UserSpecification(string username,string password) : base(x => (
        username.Equals(x.userName) && password.Equals(x.password)
        ))
        {

        }       
    }
}
