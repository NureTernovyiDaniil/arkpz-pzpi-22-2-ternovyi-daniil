using AutoMapper;

namespace ChefMate_backend
{
    public static class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            });
        }
    }
}
