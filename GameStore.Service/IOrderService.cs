using GameStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public interface IOrderService
    {
        Order GetOrder(int id);

        List<Order> GetOrders();

        void CreateOrder(Order order);
    }
}
