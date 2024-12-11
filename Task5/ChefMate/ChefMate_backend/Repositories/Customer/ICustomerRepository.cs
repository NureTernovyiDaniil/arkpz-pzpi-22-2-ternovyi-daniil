using ChefMate_backend.Models;

namespace ChefMate_backend.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerDto>> Retrieve();
        Task<CustomerDto> Retrieve(Guid id);
        Task<bool> Insert(CustomerDto customer);
        Task<bool> Update(CustomerDto customer);
        Task<bool> Delete(CustomerDto customer);
        Task<bool> Delete(Guid id);
    }
}
