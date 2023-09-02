

using System.ComponentModel.DataAnnotations.Schema;

namespace CyberSecurityNextApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [NotMapped]
        public string Password { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PassworSalt { get; set; } = new byte[0];

        public string Role { get; set; } = string.Empty;

        public UserStatus IsActive { get; set; }

        public int? CreatedBy { get; set; } 
        public int? UpdatedBy { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
