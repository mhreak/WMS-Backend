using Microsoft.AspNetCore.Identity;
using WMS.Domain.Entities.Users;

namespace WMS.Application.Common.Auth.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    Task<User?> GetByEmailAsync(string email);
}
