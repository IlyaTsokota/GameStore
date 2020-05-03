using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.Areas.Admin.ViewModels.SupplyViewModels;
using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.Controllers
{
    public class SupplyController : Controller
    {
        private readonly ISupplyService _supplyService;

        private readonly ISupplierService _supplierService;

        private readonly IProductService _productService;

        private const int _pageSize = 10;

        public SupplyController(ISupplyService supplyService, IProductService productService, ISupplierService supplierService)
        {
            _supplyService = supplyService;
            _productService = productService;
            _supplierService = supplierService;
        }

        public ActionResult Index(int page = 1)
        {
            var model = GetSupplyList(page, null);
            return View(model);
        }

        public ActionResult GetSupplies(int? supplierId, int page = 1)
        {
            var model = GetSupplyList(page, supplierId);
            return PartialView("_Supplies", model);
        }

        public IndexSupplyViewModel GetSupplyList(int page, int? supplierId)
        {
            var suppliers = _supplierService.GetSuppliers();
            var supplierList = new List<SelectListItem>(
                suppliers.Select(x => new SelectListItem { Text = x.Name, Value = x.SupplierId.ToString() }));
            var supplies = _supplyService.GetSupplies(supplierId);
            var suppliesMapping = supplies.Select(Mapper.Map<Supply, SupplyViewModel>).Skip((page-1)*_pageSize).Take(_pageSize).ToList();
            var pager = new Pager(page, supplies.Count(), _pageSize);

            var model = new IndexSupplyViewModel
            {
             Supplies = suppliesMapping,
             Suppliers = supplierList,
             Pager = pager
            };

            return model;
        }
        public ActionResult Details(int id)
        {
            var supply = _supplyService.GetSupply(id);
            if (supply == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            var model = Mapper.Map<Supply, DetailsSupplyViewModel>(supply);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var suppliers = _supplierService.GetSuppliers().Select(x => new SelectListItem { Text = x.Name, Value = x.SupplierId.ToString() });
            var products = _productService.GetProducts().Select(x => new SelectListItem { Text = x.Name, Value = x.ProductId.ToString() });
            var model = new AddSupplyViewModel
            {
                Suppliers = suppliers.ToList(),
                Products = products.ToList()
            };
            return View("Add", model);
        }

        // POST: Admin/Supply/Create
        [HttpPost]
        public ActionResult Create(AddSupplyViewModel model)
        {
            model.SupplyProducts = model.SupplyProducts.Where(x => x.Quantity != 0).ToList();
            if (!model.SupplyProducts.Any())
            {
                return RedirectToAction("Index");
            }
            var supply = Mapper.Map<AddSupplyViewModel, Supply>(model);
            supply.SupplyDate = DateTime.Now;
            _supplyService.CreateSupply(supply);
            Logger.Log.Info($"{User.Identity.Name} создал новую поставку №{model.SupplyId}");
            return RedirectToAction("Details", new { id = supply.SupplyId });
        }
    }
}