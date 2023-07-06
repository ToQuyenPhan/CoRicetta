using CoRicetta.Data.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.CategoryRepo
{
    public interface ICategoryRepo
    {
        Task<List<ViewCategory>> GetCategories();
        Task CreateCategory(CategoryFormModel model);
        Task<bool> IsExitedCategory(CategoryFormModel model);
        Task UpdateCategory(CategoryFormModel model, int categoryId);
        Task<ViewCategory> GetCategoryById(int categoryId);
        Task DeleteCategory(int categoryId);
    }
}
