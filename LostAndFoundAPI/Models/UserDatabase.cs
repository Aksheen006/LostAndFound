// Author: Aljen Biliran
// Purpose: EF Core DbContext for Users (users.db).

using Microsoft.EntityFrameworkCore;

namespace LostAndFoundAPI.Models;

public class UserDatabase : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=users.db");
    }
}
