using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.Areas.Admin.ViewModels.SupplierViewModels;
using GameStore.Web.Extensions;
using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        private const int _pageSize = 10;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        // GET: Admin/Supplier
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var model = GetSupplierListModel(page);
            return View(model);
        }

        public ActionResult GetSuppliers(int page = 1, bool includeDeleted = false)
        {
            var model = GetSupplierListModel(page, includeDeleted);
            return PartialView("_Suppliers", model);
        }
        public IndexSupplierViewModel GetSupplierListModel(int page, bool isDeleted = false)
        {
            var suppliers = _supplierService.GetSuppliers(isDeleted);
            var mappingSuppliers = suppliers.Select(Mapper.Map<Supplier, SupplierViewModel>).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            var pager = new Pager(page, suppliers.Count(), _pageSize);
            var model = new IndexSupplierViewModel { Suppliers = mappingSuppliers, Pager = pager };
            return model;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SupplierViewModel model)
        {
            var supplier = Mapper.Map<SupplierViewModel, Supplier>(model);
            var errors = _supplierService.CanAddSupplier(supplier);
            ModelState.AddModelErrors(errors);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _supplierService.CreateSupplier(supplier);
            Logger.Log.Info($"{User.Identity.Name} добавил нового поставщика {model.Name}");
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Mapper.Map<Supplier, SupplierViewModel>(_supplierService.GetSupplier(id));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SupplierViewModel model)
        {
            var supplier = Mapper.Map<SupplierViewModel, Supplier>(model);
            var errors = _supplierService.CanAddSupplier(supplier);
            ModelState.AddModelErrors(errors);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _supplierService.UpdateSupplier(supplier);
            Logger.Log.Info($"{User.Identity.Name} изменил поставщика №{supplier.SupplierId} - {model.Name}");
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var supplier = _supplierService.GetSupplier(id);
            if (supplier == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }
            var model = Mapper.Map<Supplier, DetailsSupplierViewModel>(supplier);
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var supplier = _supplierService.GetSupplier(id);
            supplier.IsDeleted = true;
            _supplierService.UpdateSupplier(supplier);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Restore(int id)
        {
            var supplier = _supplierService.GetSupplier(id);
            supplier.IsDeleted = false;
            _supplierService.UpdateSupplier(supplier);
            return RedirectToAction("Index");
        }
    }
}