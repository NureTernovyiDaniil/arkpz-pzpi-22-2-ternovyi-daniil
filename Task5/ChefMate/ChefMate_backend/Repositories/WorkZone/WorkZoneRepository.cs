using AutoMapper;
using ChefMate_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefMate_backend.Repositories
{
    public class WorkZoneRepository : IWorkZoneRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public WorkZoneRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            if(id != null && id != Guid.Empty)
            {
                var workZone = await _context.WorkZones.FirstOrDefaultAsync(x => x.Id == id);
                if(workZone != null)
                {
                    _context.WorkZones.Remove(workZone);

                    return await _context.SaveChangesAsync() > 0;
                }
            }

            return false;
        }

        public async Task<bool> Insert(WorkZoneDto workZoneDto)
        {
            if(workZoneDto != null)
            {
                var workZone = _mapper.Map<WorkZone>(workZoneDto);
                await _context.WorkZones.AddAsync(workZone);

                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<List<WorkZoneDto>> Retrieve()
        {
            return _mapper.Map<List<WorkZoneDto>>(await _context.WorkZones.ToListAsync());
        }

        public async Task<WorkZoneDto> Retrieve(Guid id)
        {
            return _mapper.Map<WorkZoneDto>(await _context.WorkZones.FirstOrDefaultAsync(x=>x.Id == id));
        }

        public async Task<bool> Update(WorkZoneDto workZoneDto)
        {
            if(workZoneDto != null)
            {
                var workZone = _mapper.Map<WorkZone>(workZoneDto);
                var exWorkZone = await _context.WorkZones.FirstOrDefaultAsync(x => x.Id == workZone.Id);
                if (workZone != null)
                {
                    _context.WorkZones.Update(workZone);

                    return await _context.SaveChangesAsync() > 0;
                }
            }

            return false;
        }

        public async Task<bool> IsExistInOrganization(Guid workZoneId, Guid organizationId)
        {
            var result = _context.WorkZones
                .Where(x=>x.Id == workZoneId && x.OrganizationId == organizationId)
                .FirstOrDefaultAsync();

            return result != null;
        }
    }
}
