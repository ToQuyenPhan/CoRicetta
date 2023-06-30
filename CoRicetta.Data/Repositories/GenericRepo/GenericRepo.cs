using CoRicetta.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.GenericRepo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly CoRicettaDBContext context;
        private DbSet<T> _entities;

        public GenericRepo(CoRicettaDBContext context)
        {
            this.context = context;
            _entities= context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.AsQueryable().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? includes)
        {
            return await AsQueryableWithIncludes(includes).AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        private IQueryable<T> AsQueryableWithIncludes(Expression<Func<T, object>>[]? includes)
        {
            var query = _entities.AsQueryable();
            if (includes == null) return query;

            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            return query;
        }

        public virtual async Task<IList<T>> WhereAsync(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {
            List<T> list;
            var query = _entities.AsQueryable();
            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);

            list = await query.Where(predicate).AsNoTracking().ToListAsync<T>();
            return list;
        }

        public async Task<IList<T>> WhereAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? includes)
        {
            return await AsQueryableWithIncludes(includes).Where(predicate).AsNoTracking().ToListAsync();
        }

        public virtual async Task UpdateAsync(T updated)
        {
            context.Attach(updated).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
