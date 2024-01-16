using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.InterFace
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetByID(int id);
        T Create(T entity);
        T Update(T entity);
        bool Delete(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

    }
}
