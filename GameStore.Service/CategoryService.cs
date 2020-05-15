using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository CategoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = CategoryRepository;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ValidationResult> CanAddCategory(Category newCategory)
        {
            Category category = newCategory.CategoryId == 0 
                ? _categoryRepository.Get(x => x.Name == newCategory.Name) 
                : _categoryRepository.Get(
                   (x => x.Name == newCategory.Name && x.CategoryId != newCategory.CategoryId));
            if (category != null)
            {
                yield return new ValidationResult("Категория с данным названием уже существует!!");
            }
        }

        public void CreateCategory(Category category)
        {
            _categoryRepository.Add(category);
            _unitOfWork.Commit();
        }

        public void DeleteCategory(Category category)
        {
            _categoryRepository.Delete(category);
            _unitOfWork.Commit();
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
            _unitOfWork.Commit();
        }
        
        public Category GetCategory(int id) => _categoryRepository.GetById(id);

        public List<Category> GetCategories() => _categoryRepository.GetAll().ToList();

      
    }
}
