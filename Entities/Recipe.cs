using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        [RegularExpression("[a-zA-Z ]+", ErrorMessage = "Only letters please")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters please")]
        [Required]
        public string Name { get; set; }
        [RegularExpression("[a-zA-Z ,.]+", ErrorMessage = "Only letters please")]
        [MinLength(15, ErrorMessage = "Please provide a 15 character or longer description")]
        [Required]
        public string Description { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
        }

        public string ShortDescription()
        {
            if (Description.Length < 50)
            {
                return Description;
            }
            else
            {
                return Description.Substring(0, 48) + "...";
            }
        }

    }
}
