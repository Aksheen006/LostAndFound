// Author: Aljen Biliran
// Purpose: Business logic for registering and logging in users.

using LostAndFoundAPI.Models;
using LostAndFoundAPI.Repositories;

namespace LostAndFoundAPI.Services;

public class UserAuthService
{
    private readonly IUserRepository _repo;

    public UserAuthService(IUserRepository repo)
    {
        _repo = repo;
    }

    public User Register(User user)
    {
        var existing = _repo.GetByEmail(user.Email);
        if (existing != null)
            throw new ArgumentException("Email already exists sorry");

        return _repo.Add(user);
    }

    public User Login(string email, string password)
    {
        var user = _repo.GetByEmail(email);
        if (user == null)
            throw new ArgumentException("User is not found");

        if (user.Password != password)
            throw new ArgumentException("The password is invalid");

        return user;
    }
}
