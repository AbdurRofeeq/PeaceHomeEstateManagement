using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeaceHomeEstateManagement.Dto;
using PeaceHomeEstateManagement.Models;

namespace PeaceHomeEstateManagement.Contract.Service
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserResponseDto> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<UserResponseDto?> LoginAsync(LoginUserDto loginUserDto);
        Task<UserResponseDto?> GetUserByIdAsync(Guid id);
        Task DeleteUserAsync(Guid userId);
    }
}