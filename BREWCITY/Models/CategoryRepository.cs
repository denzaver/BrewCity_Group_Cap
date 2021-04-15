using BREWCITY.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class CategoryRepository : ICategoryRepository // this class implements the ICategory interface
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories => _context.Categories;
    }
}
