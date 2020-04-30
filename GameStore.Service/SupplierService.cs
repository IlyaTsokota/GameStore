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
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }

        public List<Supplier> GetSuppliers(bool includeDeleted = false)
        {
            var suppliers = !includeDeleted
                                ? _supplierRepository.GetAll()
                                : _supplierRepository.GetMany(x => x.IsDeleted);
            return suppliers.OrderBy(x => x.Name).ToList();
        }

        public Supplier GetSupplier(int id)
        {
            var supplier = _supplierRepository.GetById(id);
            return supplier;
        }

        public Supplier GetSupplier(string name)
        {
            var supplier = _supplierRepository.Get(x => x.Name == name);
            return supplier;
        }

        public IEnumerable<ValidationResult> CanAddSupplier(Supplier newSupplier)
        {
            var supplier = newSupplier.SupplierId == 0
                               ? _supplierRepository.Get(x => x.Name == newSupplier.Name)
                               : _supplierRepository.Get(
                                   x => x.Name == newSupplier.Name && x.SupplierId != newSupplier.SupplierId);

            if (supplier != null)
            {
                yield return new ValidationResult("Поставщик с даным именем уже существует!");
            }
        }

        public void CreateSupplier(Supplier supplier)
        {
            _supplierRepository.Add(supplier);
            _unitOfWork.Commit();
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _supplierRepository.Update(supplier);
            _unitOfWork.Commit();
        }

        public void DeleteSupplier(Supplier supplier)
        {
            supplier.IsDeleted = true;
            _supplierRepository.Update(supplier);
            _unitOfWork.Commit();
        }

        public void RestoreSupplier(Supplier supplier)
        {
            supplier.IsDeleted = false;
            _supplierRepository.Update(supplier);
            _unitOfWork.Commit();
        }

    }
}
