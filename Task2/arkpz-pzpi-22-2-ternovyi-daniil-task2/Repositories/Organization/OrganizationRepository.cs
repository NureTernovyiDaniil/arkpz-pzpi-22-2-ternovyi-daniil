using AutoMapper;
using ChefMate_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefMate_backend.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrganizationRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(OrganizationDto organization)
        {
            var org = _mapper.Map<Organization>(organization);
            await _dbContext.Organizations.AddAsync(org);
            var result = await _dbContext.SaveChangesAsync();
            organization.Id = org.Id;
            return result > 0;
        }

        public async Task<List<OrganizationDto>> Retrieve()
        {
            var orgs = await _dbContext.Organizations.ToListAsync();
            return _mapper.Map<List<OrganizationDto>>(orgs);
        }

        public async Task<OrganizationDto> Retrieve(Guid id)
        {
            var org = await _dbContext.Organizations.FirstOrDefaultAsync(x => x.Id == id);
            if(org != null)
            {
                return _mapper.Map<OrganizationDto>(org);
            }

            return new OrganizationDto();
        }

        public async Task<bool> Update(OrganizationDto organization)
        {
            if(organization != null)
            {
                var org = _mapper.Map<Organization>(organization);
                _dbContext.Organizations.Update(org);
                var updateResult = await _dbContext.SaveChangesAsync();

                return updateResult > 0;
            }

            return false;
        }
    }
}
