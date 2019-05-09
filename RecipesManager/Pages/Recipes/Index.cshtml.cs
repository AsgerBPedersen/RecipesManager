using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipesManager.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Recipe Recipe  { get; set; }
        public List<Recipe> Recipes { get; set; }
        public SelectList Ingredients { get; set; }
        private readonly IngredientRepository ir;
        private readonly RecipeRepository rr;

        public IndexModel()
        {
            ir = new IngredientRepository();
            rr = new RecipeRepository();
        }
        public void OnGet()
        {
            Ingredients = new SelectList(ir.GetAllIngredients(), "Id", "Name");
            Recipes = rr.GetAllRecipesWithIngredients();
        }

        public IActionResult OnPost()
        {
            var selectedIngredients = Request.Form["selectedIngredients"].ToList();

            //laver nye ingredients i Recipes fra de valgte values i selectlisten
            selectedIngredients.ForEach(i => Recipe.Ingredients.Add(new Ingredient { Id = int.Parse(i) }));

            if (ModelState.IsValid)
            {
                int newId = rr.NewRecipe(Recipe);
                return RedirectToPage("/Recipes/edit", new { id = newId });

            }
            Ingredients = new SelectList(ir.GetAllIngredients(), "Id", "Name");
            Recipes = rr.GetAllRecipes();
            return Page();
        }

        public IActionResult OnGetDelete(int id)
        {
            rr.DeleteRecipe(id);
            return Redirect("/Recipes/index");
        }
    }
}