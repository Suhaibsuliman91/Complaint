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
    public class RepositoryStatus:Repository<Status>, IRepositoryStatus
    {
        public RepositoryStatus(AppDBContext dBContext):base(dBContext)
        {
        }
    }
}
