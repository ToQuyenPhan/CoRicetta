﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.GenericRepo
{
    public interface IGenericRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task CreateAsync(T entity);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? includes);
        Task<IList<T>> WhereAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? includes);
        Task<IList<T>> WhereAsync(Expression<Func<T, bool>> predicate, params string[] navigationProperties);
        Task CreateRangeAsync(List<T> entities);
        Task UpdateAsync(T updated);
        Task UpdateRangeAsync(IList<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IList<T> entities);
    }
}
