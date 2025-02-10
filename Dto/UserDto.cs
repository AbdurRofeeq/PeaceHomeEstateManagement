using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaceHomeEstateManagement.Dto
{
    public struct CreateUserDto
    {
        public string Email {get; set;}
        public string Password {get; set;}
    }

    public struct UpdateUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string? NewPassword { get; set; }
    }

    public class LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}