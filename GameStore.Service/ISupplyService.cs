using GameStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public interface ISupplyService
    {
        Supply GetSupply(int id);

        List<Supply> GetSupplies(int? supplierId = null);

        void CreateSupply(Supply supply);
    }
}
