using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PermutationsService.Data.DataAccess.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        T Add(T t);
        Task<T> AddAsyn(T t);
        void Delete(T entity);
        Task<int> DeleteAsyn(T entity);
        T Find(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);
        T Get(int id);
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsyn();
        Task<T> GetAsync(int id);
        void Save();
        Task<int> SaveAsync();
    }
}
