using CoRicetta.Data.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<ViewCategory>> GetCategories();
    }
}
