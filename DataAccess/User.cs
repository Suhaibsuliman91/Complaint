using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int UserTypeID { get; set; }
        public virtual UserType? UserType { get; set; }
        public virtual ICollection<Complaint>? Complaint { get; set; }
    }

}
