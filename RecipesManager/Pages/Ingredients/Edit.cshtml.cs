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
        public void OnGet(int id)
        {
            Ingredient = ir.GetIngredient(id);
        }

        public IActionResult OnPost()
        {
            ir.UpdateIngredient(Ingredient);
            return Redirect("/ingredients/index");
        }
    }
}