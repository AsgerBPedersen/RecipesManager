using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipesManager.Pages.Ingredients
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Ingredient Ingredient { get; set; }
        private readonly IngredientRepository ir;
        public EditModel()
        {
            ir = new IngredientRepository();
        }
        public IActionResult OnGet(int id)
        {
            Ingredient = ir.GetIngredient(id);
            if (Ingredient == null)
            {
               return RedirectToPage("/Errors/default", new { errorCode = 500 });
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ir.UpdateIngredient(Ingredient);

                return Redirect("/ingredients/index");
            }
            
            return Page();
        }
    }
}