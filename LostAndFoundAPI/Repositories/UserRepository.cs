// Author: Aljen Biliran
// Purpose: EF Core repository for users.

using LostAndFoundAPI.Models;

namespace LostAndFoundAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDatabase _db = new UserDatabase();

    public User? GetByEmail(string email)
    {
        return _db.Users.FirstOrDefault(u => u.Email == email);
    }

    public User Add(User user)
    {
        _db.Users.Add(user);
        _db.SaveChanges();
        return user;
    }
}
