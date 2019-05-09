﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipesManager.Pages
{
    public class IndexModel : PageModel
    {
        public List<Recipe> Recipes { get; set; }
        private readonly RecipeRepository rr;
        public IndexModel()
        {
            rr = new RecipeRepository();
        }
        public void OnGet()
        {
            Recipes = rr.GetAllRecipes();
        }
    }
}