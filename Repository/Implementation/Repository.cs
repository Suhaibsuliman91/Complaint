using Data;
using Microsoft.EntityFrameworkCore;
using Repository.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDBContext _AppDBContext;
        public Repository(AppDBContext AppDBContext)
        {
            _AppDBContext = AppDBContext;
        }
        public T Create(T entity)
        {
            return _AppDBContext.Set<T>().Add(entity).Entity;
        }

      

        public IEnumerable<T> GetAll()
        {
            return _AppDBContext.Set<T>().Where(e => e.IsDeleted == false);
        }

        public T GetByID(int id)
        {
            return _AppDBContext.Set<T>().Where(e => e.ID == id && e.IsDeleted == false).FirstOrDefault();
        }

        public T Update(T entity)
        {
            return _AppDBContext.Set<T>().Update(entity).Entity;
        }

        public bool Delete(T entity)
        {
            return _AppDBContext.Set<T>().Remove(entity) != null;

        }
    }
}
