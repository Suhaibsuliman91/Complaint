using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Complaint : BaseEntity
    {
        public string DescriptionEn {  get; set; }

        public string DescriptionAr { get; set; }
        //public string UserName { get; set; }
        //public string UserNumber { get; set; }
        public string Attachment { get; set; }
        public int UserID { get; set; }
        public int StatusID { get; set; }

        public virtual User? User { get; set; }
        public virtual Status? Status { get; set; }

        public virtual ICollection<Demand>? Demand { get; set; }
    }
}
