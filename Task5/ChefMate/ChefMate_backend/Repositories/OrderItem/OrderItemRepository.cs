using AutoMapper;
using ChefMate_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefMate_backend.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderItemRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid orderItemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem == null)
            {
                return false;
            }

            _context.OrderItems.Remove(orderItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(OrderItemDto orderItemDto)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            _context.OrderItems.Remove(orderItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Insert(OrderItemDto orderItemDto)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            await _context.OrderItems.AddAsync(orderItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<OrderItem>> Retrieve()
        {
            var orderItems = await _context.OrderItems.ToListAsync();
            return orderItems;
        }

        public async Task<OrderItem> Retrieve(Guid orderItemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem == null)
            {
                throw new KeyNotFoundException("Order item not found.");
            }

            return orderItem;
        }

        public async Task<bool> Update(OrderItemDto orderItemDto)
        {
            var existingOrderItem = await _context.OrderItems.FindAsync(orderItemDto.Id);
            if (existingOrderItem == null)
            {
                throw new KeyNotFoundException("Order item not found.");
            }

            _mapper.Map(orderItemDto, existingOrderItem);
            _context.OrderItems.Update(existingOrderItem);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
