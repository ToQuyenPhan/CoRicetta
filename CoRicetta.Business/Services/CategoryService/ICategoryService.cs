using CoRicetta.Data.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<ViewCategory>> GetCategories();
        Task CreateCategory(CategoryFormModel model, string token);
        Task UpdateCategory(CategoryFormModel model, string token, int categoryId);
        Task DeleteCategory(string token, int categoryId);
    }
}
