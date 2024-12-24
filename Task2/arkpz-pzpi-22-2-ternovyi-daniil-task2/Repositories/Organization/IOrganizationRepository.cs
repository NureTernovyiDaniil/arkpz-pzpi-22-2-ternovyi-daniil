using ChefMate_backend.Models;

namespace ChefMate_backend.Repositories
{
    public interface IOrganizationRepository
    {
        Task<List<OrganizationDto>> Retrieve();
        Task<OrganizationDto> Retrieve(Guid id);
        Task<bool> Insert(OrganizationDto organization);
        Task<bool> Delete(Guid id);
        Task<bool> Update(OrganizationDto organization);
    }
}
