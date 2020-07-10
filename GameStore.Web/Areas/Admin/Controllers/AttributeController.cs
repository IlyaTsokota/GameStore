using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.Areas.Admin.ViewModels.AttributeViewModels;
using GameStore.Web.Areas.Admin.ViewModels.CategoryViewModels;
using GameStore.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attribute = GameStore.Model.Attribute;

namespace GameStore.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class AttributeController : Controller
    {
        // GET: Admin/Attribute
        private readonly IAttributeService _attributeService;
        private readonly ICategoryService _categoryService;

        public AttributeController(IAttributeService attributeService, ICategoryService categoryService)
        {
            _attributeService = attributeService;
            _categoryService = categoryService;
        }

        // GET: Admin/Attribute
        [HttpGet]
        public ActionResult Index(int categoryId)
        {
            var category = _categoryService.GetCategory(categoryId);
            if (category == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            var model = Mapper.Map<Category, DetailsCategoryViewModel>(category);
            return View(model);
        }

        // GET: Admin/Attribute/Create
        [HttpGet]
        public ActionResult Create(int categoryId)
        {
            var model = new AttributeViewModel { CategoryId = categoryId };
            return View(model);
        }

        // POST: Admin/Attribute/Create
        [HttpPost]
        public ActionResult Create(AttributeViewModel model)
        {
            var attribute = Mapper.Map<AttributeViewModel, Attribute>(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _attributeService.CreateAttribute(attribute);
            Logger.Log.Info($"{User.Identity.Name} создал новый атрибут для категории №{model.CategoryId} с названием {model.Name}");
            return RedirectToAction("Index", new { categoryId = attribute.CategoryId });
        }

        // GET: Admin/Attribute/Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var attribute = _attributeService.GeAttribute(id);
            if (attribute == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            var model = Mapper.Map<Attribute, AttributeViewModel>(attribute);
            return View(model);
        }

        // POST: Admin/AttributeEdit
        [HttpPost]
        public ActionResult Edit(AttributeViewModel model)
        {
            var attribute = Mapper.Map<AttributeViewModel, Attribute>(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _attributeService.UpdateAttribute(attribute);
            Logger.Log.Info($"{User.Identity.Name} изменил атрибут для категории №{model.CategoryId} - {model.Name}");
            return RedirectToAction("Index", new { categoryId = attribute.CategoryId });
        }
    }
}