using CyberSecurityNextApi.Dtos.Category;
using Microsoft.Data.SqlClient;

namespace CyberSecurityNextApi.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccesseor;
        private readonly IMapper _mapper;
        public CategoryService(IHttpContextAccessor httpContextAccesseor, DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _httpContextAccesseor = httpContextAccesseor;
            _context = context;

        }

        private int GetUserId() => int.Parse(_httpContextAccesseor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        private string? GetDuplicateEntryErrorMessage(DbUpdateException ex)
        {
            var sqlException = ex.InnerException as SqlException;

            if (sqlException != null)
            {
                string errorMessage = sqlException.Message;
                int startIndex = errorMessage.IndexOf("(") + 1;
                int endIndex = errorMessage.IndexOf(")");
                string duplicateValues = errorMessage[startIndex..endIndex];

                return $"Duplicate Value: {duplicateValues}";
            }
            return null;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory)
        {
            var response = new ServiceResponse<List<GetCategoryDto>>();

            var category = _mapper.Map<Category>(newCategory);

            category.CreatedBy = GetUserId();

            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                response.Data = await _context.Categories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToListAsync();
            }

            catch (DbUpdateException ex)
            {
                var errorMessage = GetDuplicateEntryErrorMessage(ex);

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

        public async Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(int id)
        {
            var response = new ServiceResponse<List<GetCategoryDto>>();

            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception("Category with id '{updatedCategory.Id}' not found");

                _context.Categories.Remove(category);

                await _context.SaveChangesAsync();

                response.Data = await _context.Categories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            var response = new ServiceResponse<List<GetCategoryDto>>();

            var categories = await _context.Categories.ToListAsync();

            response.Data = categories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryById(int id)
        {
            var response = new ServiceResponse<GetCategoryDto>();

            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);

            response.Data = _mapper.Map<GetCategoryDto>(category);

            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedCategory)
        {
            var response = new ServiceResponse<GetCategoryDto>();

            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == updatedCategory.Id) ?? throw new Exception("Category with id '{updatedCategory.Id}' not found");

                _mapper.Map(updatedCategory, category);

                category.CategoryName = updatedCategory.CategoryName;
                category.Menu = updatedCategory.Menu;
                category.ParentId = updatedCategory.ParentId;
                category.IsActive = updatedCategory.IsActive;
                category.UpdatedBy = GetUserId();

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCategoryDto>(category);
            }
            catch (DbUpdateException ex)
            {
                var errorMessage = GetDuplicateEntryErrorMessage(ex);

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