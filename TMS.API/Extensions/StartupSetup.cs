using AutoMapper;
using TMS.BLL.Mapper;

namespace TMS.API.Extensions;

public static class StartupSetup
{
    public static void AddTMSAutoMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}
