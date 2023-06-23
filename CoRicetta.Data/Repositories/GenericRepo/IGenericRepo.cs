using CoRicetta.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.GenericRepo
{
    public interface IGenericRepo<T> where T : class
    {
        Task CreateAsync(T entity);
        IEnumerable<T> GetAll();
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? includes);
        Task<IList<T>> WhereAsync(Expression<Func<T, bool>> predicate, params string[] navigationProperties);
        Task<IList<T>> WhereAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? includes);
    }
}
