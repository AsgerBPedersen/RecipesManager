using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipesManager.Pages
{
    public class DetailsModel : PageModel
    {
        public Recipe Recipe { get; set; }
        private readonly RecipeRepository rr;
        public DetailsModel()
        {
            rr = new RecipeRepository();
        }
        public void OnGet(int id)
        {
            Recipe = rr.GetRecipe(id);
        }
    }
}