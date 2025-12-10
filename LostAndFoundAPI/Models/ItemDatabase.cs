using System;
using Microsoft.EntityFrameworkCore;
//ItemDatabase Class Author: Samuel Gopie
namespace LostAndFoundAPI.Models;

public class ItemDatabase : DbContext
{
    public DbSet<Item> Items { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=items.db");
    }

}
