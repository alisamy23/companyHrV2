using System.Collections.Generic;
using System.Linq.Expressions;

namespace RepositoryUsingEFinMVC.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(params Expression<Func<T , object>>[] expressionList);
        T GetById(object id, params Expression<Func<T, object>>[] expressionList);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}