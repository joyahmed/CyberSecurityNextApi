

using CyberSecurityNextApi.Dtos.Category;

namespace CyberSecurityNextApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<UpdateUserDto, User>();

            CreateMap<Category, GetCategoryDto>();
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }

    }
}
