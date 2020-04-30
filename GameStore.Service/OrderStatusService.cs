using GameStore.Data.Repositories;
using GameStore.Model;

using System.Collections.Generic;
using System.Linq;

namespace GameStore.Service
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        public OrderStatusService(IOrderStatusRepository orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
        }
        public List<OrderStatus> GetOrderStatuses()
        {
            return _orderStatusRepository.GetAll().ToList();
        }
    }
}
