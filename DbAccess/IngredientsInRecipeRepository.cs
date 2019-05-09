using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbAccess
{
    public class IngredientsInRecipeRepository : BaseRepository
    {
        public int AddNewIngredientsInRecipe(List<Ingredient> ingredients, int recipeId)
        {
            string q = "insert into IngredientsInRecipe (IngredientId, RecipeId, Amount, Unit) values ";
            for (int i = 0; i < ingredients.Count; i++)
            {
                q += $"({ingredients[i].Id}, {recipeId}, {ingredients[i].Amount}, {(int)ingredients[i].Unit})";
                if (i == ingredients.Count - 1)
                {
                    q += ";";
                }
                else
                {
                    q += ",";
                }
            }
            return ExecuteNonQuery(q);
        }
        public int RemoveIngredientFromRecipe(int ingredientId, int recipeId)
        {
            return ExecuteNonQuery($"delete from IngredientsInRecipe where IngredientId = {ingredientId} and RecipeId = {recipeId};");
        }
    }
}
