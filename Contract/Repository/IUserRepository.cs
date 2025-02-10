using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PeaceHomeEstateManagement.Models;

namespace PeaceHomeEstateManagement.Contract.Repository
{
    public interface IUserRepository
    {
        Task<User> LoginAsync (string email, string password);
        Task<User> GetUserByIdAsync(Guid Id);
        Task<IList<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(Expression<Func<User, bool>> expression);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
        Task<User> CreateUserAsync(User user);
    }
}