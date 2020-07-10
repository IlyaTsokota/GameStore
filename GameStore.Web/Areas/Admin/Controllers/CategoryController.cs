using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.Areas.Admin.ViewModels.CategoryViewModels;
using GameStore.Web.Extensions;
using GameStore.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private const int _pageSize = 10;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: Admin/Category
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var model = GetModelList(page);
            return View(model);
        }

        public ActionResult GetCategories(int page = 1)
        {
            var model = GetModelList(page);
            return PartialView("_Categories", model);
        }

        public IndexCategoryViewModel GetModelList(int page)
        {
            var categories = _categoryService.GetCategories();
            var mappingCategories = categories.Select(Mapper.Map<Category, CategoryViewModel>).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            var pager = new Pager(page, categories.Count(), _pageSize);
            var model = new IndexCategoryViewModel { Categories = mappingCategories, Pager = pager };
            return model;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateCategoryViewModel model)
        { 
            var category = Mapper.Map<CreateCategoryViewModel, Category>(model);
            var errors = _categoryService.CanAddCategory(category);
            ModelState.AddModelErrors(errors);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _categoryService.CreateCategory(category);
            Logger.Log.Info($"{User.Identity.Name} создал новую категорию {model.Name}");
            return RedirectToAction("Index", "Attribute", new { categoryId = category.CategoryId });
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = _categoryService.GetCategory(id);
            var model = Mapper.Map<Category, EditCategoryViewModel>(category);
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var category = Mapper.Map<EditCategoryViewModel, Category>(model);
            var errors = _categoryService.CanAddCategory(category);
            ModelState.AddModelErrors(errors);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _categoryService.UpdateCategory(category);
            Logger.Log.Info($"{User.Identity.Name} изменил название категории №{model.CategoryId} на {model.Name}");
            return RedirectToAction("Index", "Category");
        }
    }
}