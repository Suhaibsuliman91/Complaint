using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Map
{
    public class StatusMap
    {
        public StatusMap(EntityTypeBuilder<Status> entityBuilder)
        {
            entityBuilder.HasKey(t => t.ID);
            entityBuilder.Property(t => t.Name).IsRequired();
            
            entityBuilder.HasMany(t => t.Complaint).WithOne(u => u.Status).HasForeignKey(e => e.StatusID)
                .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
