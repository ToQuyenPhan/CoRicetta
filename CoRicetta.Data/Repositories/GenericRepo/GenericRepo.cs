using CoRicetta.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
    }
}
