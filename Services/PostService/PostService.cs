using CyberSecurityNextApi.Dtos.PostDtos;

namespace CyberSecurityNextApi.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IDuplicateEntryHandler _duplicateEntryHandler;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileUploadService _fileUploadService;
        private readonly ISlugService _slugService;
        private readonly IUserIdentityService _userIdentityService;

        public PostService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, IDuplicateEntryHandler duplicateEntryHandler, IWebHostEnvironment webHostEnvironment, IFileUploadService fileUploadService, ISlugService slugService, IUserIdentityService userIdentityService)
        {
            _userIdentityService = userIdentityService;
            _slugService = slugService;
            _fileUploadService = fileUploadService;
            _webHostEnvironment = webHostEnvironment;
            _duplicateEntryHandler = duplicateEntryHandler;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;

        }

        // public async Task<ServiceResponse<List<GetPostDto>>> AddPost(string thumbImageUrl, string slideImageUrl, string documentFileUrl, AddPostDto newPost)

        public async Task<ServiceResponse<List<GetPostDto>>> GetAllPosts()
        {
            var response = new ServiceResponse<List<GetPostDto>>();

            var posts = await _context.Posts.ToListAsync();

            response.Data = posts.Select(c => _mapper.Map<GetPostDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<List<GetPostDto>>> AddPost(AddPostDto newPost)
        {
            var response = new ServiceResponse<List<GetPostDto>>();

            var post = _mapper.Map<Post>(newPost);

            post.Slug = _slugService.GenerateSlug(newPost.PostTitle);
            post.CreatedBy = _userIdentityService.GetCurrentUserId();

            post.ThumbImageUrl = newPost.ThumbImageUrl;
            post.SlideImageUrl = newPost.SlideImageUrl;
            post.DocumentFileUrl = newPost.DocumentFileUrl;

            // string wwwRootPath = _webHostEnvironment.WebRootPath;

            // string title = newPost.PostTitle;

            // post.ThumbImageUrl = await _fileUploadService.UploadFileAsync(wwwRootPath, newPost.ThumbImageFile, @"images\thumbs", title);

            // post.SlideImageUrl = await _fileUploadService.UploadFileAsync(wwwRootPath, newPost.SlideImageFile, @"images\slider", title);

            // post.DocumentFileUrl = await _fileUploadService.UploadFileAsync(wwwRootPath, newPost.DocumentFile, @"files", title);

            try
            {
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                response.Data = await _context.Posts.Select(c => _mapper.Map<GetPostDto>(c)).ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                var errorMessage = _duplicateEntryHandler.GetDuplicateEntryErrorMessage(ex);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    response.Message = errorMessage;
                }
                else
                {
                    response.Message = "An error occurred while saving the record.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<ServiceResponse<List<GetPostDto>>> DeletePost(int id)
        {
            var response = new ServiceResponse<List<GetPostDto>>();

            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception("Post with id '{id}' not found");

                _context.Remove(post);

                await _context.SaveChangesAsync();

                response.Data = await _context.Posts.Select(c => _mapper.Map<GetPostDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }



        public async Task<ServiceResponse<GetPostDto>> GetPostById(int id)
        {
            var response = new ServiceResponse<GetPostDto>();

            var post = await _context.Posts.SingleOrDefaultAsync(c => c.Id == id);

            response.Data = _mapper.Map<GetPostDto>(post);

            return response;
        }

        public async Task<ServiceResponse<GetPostDto>> UpdatePost(UpdatePostDto updatedPost)
        {
            var response = new ServiceResponse<GetPostDto>();

            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == updatedPost.Id) ?? throw new Exception("Post with id '{updatedPost.Id}' not found");

                _mapper.Map(updatedPost, post);

                post.Slug = _slugService.GenerateSlug(updatedPost.PostTitle);
                post.UpdatedBy = _userIdentityService.GetCurrentUserId();


                post.ThumbImageUrl = updatedPost.ThumbImageUrl;
                post.SlideImageUrl = updatedPost.SlideImageUrl;
                post.DocumentFileUrl = updatedPost.DocumentFileUrl;

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetPostDto>(post);
            }
            catch (DbUpdateException ex)
            {
                var errorMessage = _duplicateEntryHandler.GetDuplicateEntryErrorMessage(ex);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    response.Message = errorMessage;
                }
                else
                {
                    response.Message = "An error occurred while saving the record.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }
    }
}