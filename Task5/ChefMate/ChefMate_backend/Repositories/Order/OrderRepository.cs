using AutoMapper;
using ChefMate_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefMate_backend.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            _context.Orders.Remove(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Insert(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _context.Orders.AddAsync(order);

            orderDto.Id = order.Id;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Order>> Retrieve()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();

            if(orders.Any())
            {
                foreach(var order in orders)
                {
                    if(order.OrderItems.Any())
                    {
                        foreach (var item in order.OrderItems)
                        {
                            item.MenuItem = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == item.MenuItemId);
                        }
                    }
                }
            }

            return orders;
        }

        public async Task<List<Order>> RetrieveByPeriod(DateTime startDate, DateTime endDate)
        {
            var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
            .ToListAsync();

            return orders;
        }

        public async Task<List<Order>> RetrieveByDate(DateTime targetDate)
        {
            var ordersForDate = await _context.Set<Order>()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .Where(o => o.OrderDate.Date == targetDate.Date)
            .ToListAsync();

            return ordersForDate;
        }

        public async Task<Order> Retrieve(Guid orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            if(order.OrderItems.Any())
            {
                foreach (var item in order.OrderItems)
                {
                    item.MenuItem = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == item.MenuItemId);
                }
            }
           
            return order;
        }

        public async Task<bool> Update(OrderDto orderDto)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderDto.Id);

            if (existingOrder == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            _mapper.Map(orderDto, existingOrder);
            _context.Orders.Update(existingOrder);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
