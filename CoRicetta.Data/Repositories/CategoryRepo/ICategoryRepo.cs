using CoRicetta.Data.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.CategoryRepo
{
    public interface ICategoryRepo
    {
        Task<List<ViewCategory>> GetCategories();
    }
}
