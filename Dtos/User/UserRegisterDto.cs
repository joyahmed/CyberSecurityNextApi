using System.ComponentModel.DataAnnotations.Schema;

namespace CyberSecurityNextApi.Dtos.User
{
    public class UserRegisterDto
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public UserStatus IsActive { get; set; }

    }
}
