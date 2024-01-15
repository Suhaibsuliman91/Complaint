using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Map
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.HasKey(t => t.ID);
            entityBuilder.Property(t => t.Name).IsRequired(false).HasMaxLength(150);
           
            entityBuilder.Property(t => t.Mobile).IsRequired(false);
          
            entityBuilder.HasMany(t => t.Complaint).WithOne(u => u.User).HasForeignKey(e=>e.UserID)
                .OnDelete(DeleteBehavior.NoAction);
           
        }
    }
}
