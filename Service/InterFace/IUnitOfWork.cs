using Service.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.InterFace
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepositoryUser User { get; }
        public IRepositoryUserType UserType { get; }
        public IRepositoryComplaint Complaint { get; }
        public IRepositoryDemand Demand { get; }
        void Complete();
    }
}
