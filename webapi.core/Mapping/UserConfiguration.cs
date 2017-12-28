using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webapi.core.Entities;

namespace webapi.core.Mapping
{
    /// <summary>
    /// User Table Mapping
    /// </summary>
    public class UserConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserConfiguration"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public UserConfiguration(EntityTypeBuilder<UserEntities> builder)
        {
            builder.ToTable("tblUser");
            builder.HasKey(i => i.id);
            builder.Property(i => i.userName).HasMaxLength(150).IsRequired(true);
            builder.Property(i => i.password).HasMaxLength(150).IsRequired(true);           
            builder.Property(i => i.created);
            builder.Property(i => i.createdby).HasMaxLength(150);
            builder.Property(i => i.modified);
            builder.Property(i => i.modifiedby).HasMaxLength(150);
        }
    }
}

