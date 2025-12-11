using System;
using System.ComponentModel.DataAnnotations;

namespace LostAndFoundPortal.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; } 

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Lost";   // Lost or Found

        [Display(Name = "Date Posted")]
        public DateTime DatePosted { get; set; } = DateTime.Now;
    }
}
