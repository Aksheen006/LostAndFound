// Author: Aljen Biliran
// Purpose: Safe user data to return to client with no password

namespace LostAndFoundAPI.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
}
