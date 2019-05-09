using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipesManager.Pages
{
    public class IndexModel : PageModel
    {
        public List<Recipe> Recipes { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Vegetarian { get; set; } = false;
        public List<SelectListItem> Sl { get; set; }
        private readonly RecipeRepository rr;
        public IndexModel()
        {
            rr = new RecipeRepository();
            Sl = new List<SelectListItem>()
            {
                new SelectListItem { Value = "false", Text = "All" },
                new SelectListItem { Value = "true", Text = "Vegetarian" }
            };
        }
        public void OnGet()
        {
            if (Vegetarian == false)
            {
                Recipes = rr.GetAllRecipesWithIngredients();
            }
            else
            {
                Recipes = rr.GetAllRecipesWithIngredients().Where(r => r.Vegetarian == true).ToList();
            }
        }
    }
}