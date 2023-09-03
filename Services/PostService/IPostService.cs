using CyberSecurityNextApi.Dtos.PostDtos;

namespace CyberSecurityNextApi.Services.PostService
{
    public interface IPostService
    {
        Task<ServiceResponse<List<GetPostDto>>> GetAllPosts();

        Task<ServiceResponse<GetPostDto>> GetPostById(int id);

        Task<ServiceResponse<List<GetPostDto>>> AddPost(AddPostDto newPost);
        // Task<ServiceResponse<List<GetPostDto>>> AddPost(string thumbImageUrl, string slideImageUrl, string documentFileUrl, AddPostDto newPost);

        Task<ServiceResponse<GetPostDto>> UpdatePost(UpdatePostDto updatedPost);

        Task<ServiceResponse<List<GetPostDto>>> DeletePost(int id);
    }
}