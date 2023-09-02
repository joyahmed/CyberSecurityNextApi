﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CyberSecurityNextApi.Dtos.User
{
    public class GetUserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public UserStatus IsActive { get; set; }

    }
}
