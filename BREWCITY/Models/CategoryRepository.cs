using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class CategoryRepository : ICategoryRepository // this class implements the ICategory interface
    {
        public IEnumerable<Category> GetAllCategories => throw new NotImplementedException();
    }
}
