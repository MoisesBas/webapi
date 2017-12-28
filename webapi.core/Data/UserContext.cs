using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using webapi.core.Entities;

namespace webapi.core.Data
{  
    public class UserContext : DbContext
    {
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var items = new Dictionary<object, object>();

            foreach (var entry in ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                var entity = entry.Entity;
                var context = new ValidationContext(entity, items);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, context, results, true) == false)
                {
                    foreach (var result in results)
                    {
                        if (result != ValidationResult.Success)
                        {
                            throw new ValidationException(result.ErrorMessage);
                        }
                    }
                }
            }
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<UserEntities> users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            new Mapping.UserConfiguration(builder.Entity<UserEntities>());
          
            base.OnModelCreating(builder);

        }
       

    }
}
