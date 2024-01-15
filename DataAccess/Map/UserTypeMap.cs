using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Map
{
    public class UserTypeMap
    {
        public UserTypeMap(EntityTypeBuilder<UserType> entityBuilder)
        {
            entityBuilder.HasKey(t => t.ID);
            entityBuilder.Property(t => t.Name).IsRequired(false);

            entityBuilder.HasMany(t => t.Users).WithOne(u => u.UserType).HasForeignKey(e => e.UserTypeID)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
