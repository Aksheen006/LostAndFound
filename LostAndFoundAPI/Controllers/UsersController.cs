// Author: Aljen Biliran
// Purpose: API endpoints for user registration and login.

using LostAndFoundAPI.DTOs;
using LostAndFoundAPI.Models;
using LostAndFoundAPI.Repositories;
using LostAndFoundAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFoundAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserAuthService _service = new UserAuthService(new UserRepository());

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        try
        {
            var created = _service.Register(user);
            return Ok(ToDto(created));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        try
        {
            var logged = _service.Login(req.Email, req.Password);
            return Ok(ToDto(logged));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private static UserDTO ToDto(User u) =>
        new UserDTO { Id = u.Id, Username = u.Username, Email = u.Email };
}
