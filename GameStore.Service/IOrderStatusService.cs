using GameStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public interface IOrderStatusService
    {
        List<OrderStatus> GetOrderStatuses();
    }
}
