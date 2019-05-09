using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        [RegularExpression("[a-zA-Z ]+", ErrorMessage = "Only letters please")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters")]
        [Required]
        public string Name { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = "Only whole numbers")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public int Amount { get; set; }
        public IngredientType Type { get; set; }
        public Unit Unit { get; set; }
    }
}
