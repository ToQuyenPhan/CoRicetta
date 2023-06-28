using CoRicetta.Data.Context;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.CategoryRepo
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task<List<ViewCategory>> GetCategories()
        {
            var query = from c in context.Categories where c.Status.Equals(1) select c;
            List<ViewCategory> items = await query
                                          .Select(selector => new ViewCategory()
                                          {
                                              Id = selector.Id,
                                              CategoryName = selector.CategoryName,
                                              Status = selector.Status
                                          }).ToListAsync();
            return items;
        }
    }
}
