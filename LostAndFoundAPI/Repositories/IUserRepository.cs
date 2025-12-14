// Author: Aljen Biliran
// Purpose: Abstraction over user data storage 

using LostAndFoundAPI.Models;

namespace LostAndFoundAPI.Repositories;

public interface IUserRepository
{
    User? GetByEmail(string email);
    User Add(User user);
}
