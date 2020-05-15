using GameStore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public interface IOrderService
    {
        Order GetOrder(int id);

        List<Order> GetOrders(int? statusId = null, string customerId = null, string managerId = null);

        void CreateOrder(Order order);

        IEnumerable<ValidationResult> CanAcceptOrder(Order order);

        void AcceptOrder(Order order, string managerId);

        void OrderPaid(Order order);

        void OrderCompleted(Order order);

        void CancelOrder(Order order);
    }
}
