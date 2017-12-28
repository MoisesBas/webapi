using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webapi.core.Entities;

namespace webapi.core.Mapping
{
    public class UserConfiguration
    {
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

