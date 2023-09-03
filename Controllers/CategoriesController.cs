using CyberSecurityNextApi.Dtos.Category;
using CyberSecurityNextApi.Services.CategoryService;

namespace CyberSecurityNextApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly DataContext _context;

        public CategoriesController(ICategoryService categoryService, DataContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }

        // GET: api/Categories
        [HttpGet("GetCategories")]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> GetCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> GetCategoryById(int id)
        {
            return Ok(await _categoryService.GetCategoryById(id));

        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> AddCategory(AddCategoryDto newCategory)
        {
            return Ok(await _categoryService.AddCategory(newCategory));
        }


        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> PutCategory(UpdateCategoryDto updatedCategory)
        {
            var response = await _categoryService.UpdateCategory(updatedCategory);

            if (response.Data is null) return NotFound(response);

            return Ok(response);
        }


        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategory(id);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }
    }
}
