using GameStore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
      public interface ICategoryService
    {
        Category GetCategory(int id);

        List<Category> GetCategories();

        IEnumerable<ValidationResult> CanAddCategory(Category newCategory);

        void CreateCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);

    }
}
