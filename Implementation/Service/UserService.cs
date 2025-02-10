using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeaceHomeEstateManagement.Contract.Repository;
using PeaceHomeEstateManagement.Contract.Service;
using PeaceHomeEstateManagement.Dto;
using PeaceHomeEstateManagement.Models;

namespace PeaceHomeEstateManagement.Implementation.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Email = createUserDto.Email
            };
            user.SetPassword(createUserDto.Password);  

            await _userRepository.CreateUserAsync(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public async Task<UserResponseDto> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetUserByIdAsync(updateUserDto.Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Email = updateUserDto.Email;

            if (!string.IsNullOrEmpty(updateUserDto.NewPassword))
            {
                user.SetPassword(updateUserDto.NewPassword);  
            }

            await _userRepository.UpdateUserAsync(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public async Task<UserResponseDto?> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await _userRepository.GetUserAsync(u => u.Email == loginUserDto.Email);
            if (user == null || !user.VerifyPassword(loginUserDto.Password))
            {
                return null;  
            }

            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
                return null;

            var userResponseDto = new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email
            };

            return userResponseDto;
        }


        public async Task DeleteUserAsync(Guid userId)
        {
            await _userRepository.DeleteUserAsync(userId);
        }
    }
}