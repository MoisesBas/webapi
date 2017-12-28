using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.core.Data;
using webapi.core.Entities;

namespace webapi.core.Migrations
{
  public  class UserSeedData
    {
        private UserContext _context;

        public UserSeedData(UserContext context)
        {
            _context = context;
        }

        public async Task SeedData()
        {
            if (!_context.users.Any())
            {
                var user = new UserEntities()
                {
                    userName = "user1",
                    password = "user1",                  
                    created = DateTime.Now,
                    createdby = "moises bas",
                    modified = DateTime.Now,
                    modifiedby = "moises bas"
                };
                _context.users.Add(user);
                await _context.SaveChangesAsync();
            }
        }
      
    }
}
