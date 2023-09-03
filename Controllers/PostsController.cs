using CyberSecurityNextApi.Dtos.PostDtos;

namespace CyberSecurityNextApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {

        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpGet("GetPosts")]
        public async Task<ActionResult<ServiceResponse<List<GetPostDto>>>> GetPosts()
        {
            return Ok(await _postService.GetAllPosts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetPostDto>>>> GetPostById(int id)
        {
            return Ok(await _postService.GetPostById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetPostDto>>>> AddCategory(AddPostDto newPost)
        {
            return Ok(await _postService.AddPost(newPost));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetPostDto>>>> PutCategory(UpdatePostDto updatedPost)
        {
            var response = await _postService.UpdatePost(updatedPost);

            if (response.Data is null) return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetPostDto>>>> DeletePost(int id)
        {
            var response = await _postService.DeletePost(id);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }
    }

}