using CoRicetta.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.GenericRepo
{
    public interface IGenericRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
    }
}
