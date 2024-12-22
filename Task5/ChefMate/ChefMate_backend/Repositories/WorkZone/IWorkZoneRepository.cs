using ChefMate_backend.Models;

namespace ChefMate_backend.Repositories
{
    public interface IWorkZoneRepository
    {
        Task<List<WorkZoneDto>> Retrieve();
        Task<WorkZoneDto> Retrieve(Guid id);
        Task<bool> Insert(WorkZoneDto organization);
        Task<bool> Delete(Guid id);
        Task<bool> Update(WorkZoneDto organization);
        Task<bool> IsExistInOrganization(Guid workZoneId, Guid organizationId);
    }
}
