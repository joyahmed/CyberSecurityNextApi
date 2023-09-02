

namespace CyberSecurityNextApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<User,GetUserDto>();
            CreateMap<UpdateUserDto,User>();
        }

    }
}
