using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Demand : BaseEntity
    {
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }

        public int ComplaintID { get; set; }
        public virtual Complaint? Complaint { get; set; }
    }
}
