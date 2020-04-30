using GameStore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public interface ISupplierService
    {

        List<Supplier> GetSuppliers(bool includeDeleted = false);

        Supplier GetSupplier(int id);

        Supplier GetSupplier(string name);

        IEnumerable<ValidationResult> CanAddSupplier(Supplier newSupplier);

        void CreateSupplier(Supplier supplier);

        void UpdateSupplier(Supplier supplier);

        void DeleteSupplier(Supplier supplier);

        void RestoreSupplier(Supplier supplier);
    }
}
