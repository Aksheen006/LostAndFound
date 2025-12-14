using System;

namespace LostAndFoundAPI.Models;

public class Item
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool isFound { get; set; }

}
