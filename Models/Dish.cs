using System;
using System.ComponentModel.DataAnnotations;
 
namespace CruDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        public string Chef { get; set; }

        [Required]
        [Range(1,5)]
        public int Tastiness { get; set; }

        [Required]
        [Range(1,5000)]
        public int Calories { get; set; }

        [Required]
        [MinLength(2)]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}