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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public Order GetOrder(int id)
        {
            var order = _orderRepository.Get(x => x.OrderId == id);
            return order;
        }

        public List<Order> GetOrders(int? statusId = null, string customerId = null, string managerId = null)
        {
            var orders = statusId == null
                             ? _orderRepository.GetAll()
                             : _orderRepository.GetMany(x => x.OrderStatusId == statusId);
            if (customerId != null)
            {
                orders = orders.Where(x => x.CustomerId == customerId);
            }
            else if (managerId != null)
            {
                orders = orders.Where(x => x.ManagerId == managerId);
            }

            orders = orders.OrderByDescending(x => x.OrderId);
            return orders.ToList();
        }

        public void CreateOrder(Order order)
        {
            _orderRepository.Add(order);
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAcceptOrder(Order order)
        {
            foreach (var orderDetail in order.OrderDetails)
            {
                if (orderDetail.Product == null)
                {
                    yield return new ValidationResult($@"Товар №{orderDetail.ProductId} не найден");
                }
                else if (orderDetail.Product.Quantity < orderDetail.Quantity)
                {
                    yield return new ValidationResult($@"{orderDetail.Product.Name} недостаточно на складе");
                }
            }
        }

        public void AcceptOrder(Order order, string managerId)
        {
            order.ManagerId = managerId;
            order.AcceptedDate = DateTime.Now;
            order.OrderStatusId = 2; // Payment is allowed
            _orderRepository.Update(order);

            // Reservation of products
            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.Product.Quantity -= orderDetail.Quantity;
                _productRepository.Update(orderDetail.Product);
            }

            _unitOfWork.Commit();
        }

        public void OrderPaid(Order order)
        {
            order.OrderStatusId = 3; // Order paid
            _orderRepository.Update(order);
            _unitOfWork.Commit();
        }

        public void OrderCompleted(Order order)
        {
            order.OrderStatusId = 4; // Order completed
            _orderRepository.Update(order);
            _unitOfWork.Commit();
        }

        public void CancelOrder(Order order)
        {
            order.OrderStatusId = 5; // Order canceled
            _orderRepository.Update(order);

            // Return reserved products to the stock
            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.Product.Quantity += orderDetail.Quantity;
                _productRepository.Update(orderDetail.Product);
            }

            _unitOfWork.Commit();
        }

    }
}
