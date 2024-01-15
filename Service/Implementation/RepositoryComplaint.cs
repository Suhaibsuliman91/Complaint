using Data;
using Repository;
using Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.InterFace
{
    public class RepositoryComplaint : Repository<Complaint>, IRepositoryComplaint
    {
        public RepositoryComplaint(AppDBContext dBContext):base(dBContext)
        {
            
        }
    }
}
