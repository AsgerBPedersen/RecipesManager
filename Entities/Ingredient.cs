using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public IngredientType Type { get; set; }
        public Unit Unit { get; set; }
    }
}
