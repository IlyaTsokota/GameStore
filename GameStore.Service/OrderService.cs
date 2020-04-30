using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public void CreateOrder(Order order)
        {
            _orderRepository.Add(order);
            _unitOfWork.Commit();
        }

        public Order GetOrder(int id) => _orderRepository.GetById(id);

        public List<Order> GetOrders()
        {
            var orders = _orderRepository.GetAll();
            return orders.OrderByDescending(x=>x.OrderId).ToList();
        }
    }
}
