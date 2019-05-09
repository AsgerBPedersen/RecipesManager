using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        [RegularExpression("[a-zA-ZÆØÅæøå ]+", ErrorMessage = "Only letters please")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters please")]
        [Required]
        public string Name { get; set; }
        [RegularExpression("[a-zA-ZÆØÅæøå ,.!?-]+", ErrorMessage = "Only letters please")]
        [MinLength(15, ErrorMessage = "Please provide a 15 character or longer description")]
        [Required]
        public string Description { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public string ShortDescription 
        {
            get {
                if (Description == null)
                {
                    return "";
                }
                else
                {
                    if (Description.Length < 50)
                    {
                        return Description;
                    }
                    else
                    {
                        return Description.Substring(0, 48) + "...";
                    };
                }
            }
        }

        public bool Vegetarian
        {
            get
            {
                //checker om alle ingredients er vegetariske
                var meats = new[] { IngredientType.Pork, IngredientType.Seafood, IngredientType.Poultry, IngredientType.Beef };
                if (Ingredients.Any(i => meats.Contains(i.Type)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
        }


    }
}
