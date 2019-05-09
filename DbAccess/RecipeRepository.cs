using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbAccess
{
    public class RecipeRepository : BaseRepository
    {
        public List<Recipe> GetAllRecipesWithIngredients()
        {
            List<Recipe> recipes = GetAllRecipes();
            IngredientRepository ir = new IngredientRepository();
            List<Ingredient> ingredients = ir.GetAllIngredientsFull();
            recipes.ForEach(r => r.Ingredients.AddRange(ingredients.Where(i => i.RecipeId == r.Id)));
            return recipes;
        }

        public List<Recipe> GetAllRecipes()
        {
            DataTable dt = ExecuteQuery("Select * from Recipes");
            List<Recipe> recipes = new List<Recipe>();
            foreach (DataRow row in dt.Rows)
            {
                recipes.Add(new Recipe
                {
                    Id = (int)row["Id"],
                    Name = (string)row["Name"],
                    Description = (string)row["Description"]
                });
            }
            return recipes;

        }

        public int DeleteRecipe(int id)
        {
            //fungere nu med cascade delete
            return ExecuteNonQuery($"Delete from Recipes where Id = {id}");
        }

        public Recipe GetRecipe(int id)
        {
            IngredientRepository ir = new IngredientRepository();
            List<Ingredient> ingredients = ir.GetAllIngredientsFull();
            DataTable dt = ExecuteQuery($"select * from Recipes where Id = {id};");
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            return new Recipe
            {
                Id = (int)dt.Rows[0]["Id"],
                Name = (string)dt.Rows[0]["Name"],
                Description = (string)dt.Rows[0]["Description"],
                Ingredients = ingredients.Where(i => i.RecipeId == (int)dt.Rows[0]["Id"]).ToList()
            };

        }


        public int NewRecipe(Recipe recipe)
        {
            IngredientsInRecipeRepository iirr = new IngredientsInRecipeRepository();
            int newRecipeId = ExecuteNonQueryScalar($"Insert into Recipes (Name, Description) output inserted.Id values('{recipe.Name}','{recipe.Description}');");
            if (recipe.Ingredients.Count != 0)
            {
                iirr.AddNewIngredientsInRecipe(recipe.Ingredients, newRecipeId);
            }
            return newRecipeId;
        }

        public int UpdateRecipe(Recipe recipe)
        {
            DeleteRecipe(recipe.Id);
            return NewRecipe(recipe);
        }
    }
}
