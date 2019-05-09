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
    public class EditModel : PageModel
    {
        [BindProperty]
        public Recipe Recipe { get; set; }
        public SelectList IngredientChoices { get; set; }
        private readonly RecipeRepository rr;
        private readonly IngredientRepository ir;
        private readonly IngredientsInRecipeRepository iirr;

        public EditModel()
        {
            rr = new RecipeRepository();
            ir = new IngredientRepository();
            iirr = new IngredientsInRecipeRepository();
        }
        public IActionResult OnGet(int id)
        {
           Recipe = rr.GetRecipe(id);
            if (Recipe == null)
            {
                return RedirectToPage("/Errors/default", new { errorCode = 500 });
            }
            // sætter selectlisten med de ingredients der ikke allerede er tilføjet til recipen, så man ikke kan tilføje en ingredient 2 gange.
           IngredientChoices = new SelectList(ir.GetAllIngredients().Where(i => !Recipe.Ingredients.Any(r => r.Id == i.Id)), "Id", "Name");
            return Page();
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

        public IActionResult OnGetDelete(int id, int recipeId)
        {
            iirr.RemoveIngredientFromRecipe(id, recipeId);
            return RedirectToPage("/Recipes/edit", new { id = recipeId });
        }

        public IActionResult OnPostAddIngredients(int recipeId)
        {
            //laver en liste af ingredients ud fra dem der er valgt i select listen og tilføjer dem til databasen
            var selectedIngredients = Request.Form["selectedIngredients"].ToList();
            List<Ingredient> ing = new List<Ingredient>();
            selectedIngredients.ForEach(i => ing.Add(new Ingredient { Id = int.Parse(i) }));
            iirr.AddNewIngredientsInRecipe(ing, recipeId);
            return RedirectToPage("/Recipes/edit", new { id = recipeId });
        }
    }
}