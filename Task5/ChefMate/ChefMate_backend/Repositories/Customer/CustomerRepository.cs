using AutoMapper;
using ChefMate_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefMate_backend.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> Retrieve()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> Retrieve(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<bool> Insert(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _context.Customers.AddAsync(customer);
            customerDto.Id = customer.Id;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(CustomerDto customerDto)
        {
            var existingCustomer = await _context.Customers.FindAsync(customerDto.Id);
            if (existingCustomer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            _mapper.Map(customerDto, existingCustomer);
            _context.Customers.Update(existingCustomer);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            _context.Customers.Update(customer);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            _context.Customers.Remove(customer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            _context.Customers.Remove(customer);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
