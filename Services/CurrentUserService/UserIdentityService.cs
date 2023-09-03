namespace CyberSecurityNextApi.Services.CurrentUserService
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        }
    }
}
