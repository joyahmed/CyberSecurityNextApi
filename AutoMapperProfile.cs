using CyberSecurityNextApi.Dtos.Category;
using CyberSecurityNextApi.Dtos.PostDtos;

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

            CreateMap<Post, GetPostDto>();
            CreateMap<AddPostDto, Post>();
            CreateMap<UpdatePostDto, Post>();


        }

    }
}
