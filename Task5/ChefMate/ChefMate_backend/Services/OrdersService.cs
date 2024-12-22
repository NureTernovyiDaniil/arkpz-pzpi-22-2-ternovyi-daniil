using AutoMapper;
using ChefMate_backend.Hubs;
using ChefMate_backend.Models;
using ChefMate_backend.Repositories;
using iText.Layout.Borders;
using Microsoft.AspNetCore.SignalR;

namespace ChefMate_backend.Services
{
    public class OrdersService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IHubContext<IoTHub> _iotHubContext;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public OrdersService(IOrderRepository orderRepository, IMapper mapper, 
            IMenuItemRepository menuItemRepository, IOrderItemRepository orderItemRepository,
            IHubContext<IoTHub> hubContext, IServiceScopeFactory serviceScopeFactory)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _menuItemRepository = menuItemRepository;
            _orderItemRepository = orderItemRepository;
            _iotHubContext = hubContext;
            this.serviceScopeFactory = serviceScopeFactory;
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

            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var orderFullModel = _orderRepository.Retrieve(orderId);
                    var hub = scope.ServiceProvider.GetRequiredService<IoTHub>();

                    await hub.SendMessageToGroup(orderDto.OrganizationId, orderFullModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SendToIoT(Guid orderId)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var order = await _orderRepository.Retrieve(orderId);
                var hub = scope.ServiceProvider.GetRequiredService<IoTHub>();

                await hub.SendMessageToGroup(order.OrganizationId, order);
            }
        }
    }
}
