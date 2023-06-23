using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.CategoryRepo;
using CoRicetta.Data.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepo<Category> _categoryRepo;

        public CategoryService(IGenericRepo<Category> categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }
        public async Task<IList<Category>> getAll()
        {
            try
            {
                var categories = await _categoryRepo.WhereAsync((c) => c.Status == (int)eStatus.ACTIVE);
                return categories.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException( "Something went wrong, please try again later!");
            }
        }
    }
}
