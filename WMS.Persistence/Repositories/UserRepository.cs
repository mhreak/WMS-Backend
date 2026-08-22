using Microsoft.EntityFrameworkCore;
using WMS.Application.Common.Auth.Interfaces;
using WMS.Domain.Entities.Users;
using WMS.Persistence.Context;

namespace WMS.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context) => _context = context;

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && !u.IsDeleted);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
    }
}
