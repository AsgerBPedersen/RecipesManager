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
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Ingredient Ingredient { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        private readonly IngredientRepository ir;
        public IndexModel()
        {
            ir = new IngredientRepository();
        }
        public void OnGet()
        {
            Ingredients = ir.GetAllIngredients();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ir.NewIngredient(Ingredient);
                
                return Redirect("/ingredients/index");
            }
            Ingredients = ir.GetAllIngredients();
            return Page();
        }

        public IActionResult OnGetDelete(int id)
        {
            ir.DeleteIngredient(id);
            return Redirect("/ingredients/index");
        }
    }
}