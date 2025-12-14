// Author: Aljen Biliran
// Purpose: Unit tests for UserAuthService 

using LostAndFoundAPI.Models;
using LostAndFoundAPI.Repositories;
using LostAndFoundAPI.Services;
using Xunit;

namespace LostAndFoundAPI.Tests;

public class UserAuthServiceTests
{
    private class FakeUserRepo : IUserRepository
    {
        private readonly List<User> _users = new();

        public User? GetByEmail(string email) =>
            _users.FirstOrDefault(u => u.Email == email);

        public User Add(User user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
            return user;
        }
    }

    [Fact]
    public void Register_NewEmail_UserCreated()
    {
        // Arrange
        var repo = new FakeUserRepo();
        var service = new UserAuthService(repo);
        var u = new User { Username = "Aljen", Email = "a@a.com", Password = "123" };

        // Act
        var created = service.Register(u);

        // Assert
        Assert.Equal("a@a.com", created.Email);
        Assert.True(created.Id > 0);
    }

    [Fact]
    public void Register_DuplicateEmail_Throws()
    {
        // Arrange
        var repo = new FakeUserRepo();
        var service = new UserAuthService(repo);
        service.Register(new User { Username = "X", Email = "dup@a.com", Password = "1" });

        // Act + Assert
        Assert.Throws<ArgumentException>(() =>
            service.Register(new User { Username = "Y", Email = "dup@a.com", Password = "2" }));
    }

    [Fact]
    public void Login_WrongPassword_Throws()
    {
        // Arrange
        var repo = new FakeUserRepo();
        var service = new UserAuthService(repo);
        service.Register(new User { Username = "X", Email = "x@a.com", Password = "good" });

        // Act + Assert
        Assert.Throws<ArgumentException>(() =>
            service.Login("x@a.com", "bad"));
    }
}
