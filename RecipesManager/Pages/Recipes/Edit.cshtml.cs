using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipesManager.Pages.Recipes
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Recipe Recipe { get; set; }
        [BindProperty]
        public List<Ingredient> Ingredients { get; set; }
        private readonly RecipeRepository rr;

        public EditModel()
        {
            rr = new RecipeRepository();
        }
        public void OnGet(int id)
        {
           Recipe = rr.GetRecipe(id);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                int newId = rr.UpdateRecipe(Recipe);
                return RedirectToPage("/Recipes/edit", new { id = newId });
            }
            return Page();
        }
    }
}