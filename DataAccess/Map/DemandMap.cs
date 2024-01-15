using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Map
{
    public class DemandMap
    {
        public DemandMap(EntityTypeBuilder<Demand> entityBuilder)
        {
            entityBuilder.HasKey(t => t.ID);
            entityBuilder.Property(t => t.DescriptionEn).IsRequired(false);
            entityBuilder.Property(t => t.DescriptionAr).IsRequired(false);
         


        }
    }
}
