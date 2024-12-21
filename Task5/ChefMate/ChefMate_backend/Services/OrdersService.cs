using AutoMapper;
using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChefMate_backend.Services
{
    public class OrdersService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IMenuItemRepository _menuItemRepository;

        public OrdersService(IOrderRepository orderRepository, IMapper mapper, 
            IMenuItemRepository menuItemRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _menuItemRepository = menuItemRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task HandleOrder(Guid orderId)
        {
            var order = await _orderRepository.Retrieve(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order doesn`t exists");
            }

            var menuItemIds = order.OrderItems
                .Select(x => x.MenuItemId)
                .Distinct()
                .ToList();

            var menuItems = await _menuItemRepository.Retrieve(menuItemIds);
            var menuItemsDict = menuItems.ToDictionary(m => m.Id, m => m.Price);

            foreach (var orderItem in order.OrderItems)
            {
                if (menuItemsDict.TryGetValue(orderItem.MenuItem.Id, out var price))
                {
                    orderItem.Price = price;
                    await _orderItemRepository.Update(_mapper.Map<OrderItemDto>(orderItem));
                }
            }

            order.TotalAmount = order.OrderItems.Sum(x => x.Price * x.Quantity);

            var orderDto = _mapper.Map<OrderDto>(order);
            await _orderRepository.Update(orderDto);
        }
    }
}
