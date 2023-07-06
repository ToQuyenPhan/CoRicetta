using CoRicetta.Business.Utils;
using CoRicetta.Data.Repositories.CategoryRepo;
using CoRicetta.Data.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepo _categoryRepo;
        private DecodeToken _decodeToken;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
            _decodeToken = new DecodeToken();
        }

        public async Task<List<ViewCategory>> GetCategories()
        {
            List<ViewCategory> categories = await _categoryRepo.GetCategories();
            if (categories == null) throw new NullReferenceException("Không tìm thấy loại món ăn nào!");
            return categories;
        }

        public async Task CreateCategory(CategoryFormModel model, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("Bạn không có quyền thực hiện hành động này!");
            }
            var isExisted = await _categoryRepo.IsExitedCategory(model);
            if (isExisted) throw new ArgumentException("Loại món ăn đã tồn tại!");
            await _categoryRepo.CreateCategory(model);
        }

        public async Task UpdateCategory(CategoryFormModel model, string token, int categoryId)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("Bạn không có quyền thực hiện hành động này!");
            }
            var category = await _categoryRepo.GetCategoryById(categoryId);
            if (category == null) throw new NullReferenceException("Không tìm thấy loại món ăn để chỉnh sửa");
            var isExisted = await _categoryRepo.IsExitedCategory(model);
            if (isExisted) throw new ArgumentException("Loại món ăn đã tồn tại!");
            await _categoryRepo.UpdateCategory(model, categoryId);
        }

        public async Task DeleteCategory(string token, int categoryId)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("Bạn không có quyền thực hiện hành động này!");
            }
            var category = await _categoryRepo.GetCategoryById(categoryId);
            if (category == null) throw new NullReferenceException("Không tìm thấy loại món ăn để xóa!");
            await _categoryRepo.DeleteCategory(categoryId);
        }
    }
}
