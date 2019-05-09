using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DbAccess
{
    public class IngredientRepository : BaseRepository
    {
        public List<Ingredient> GetAllIngredientsFull()
        {
            List<Ingredient> ingredients = GetAllIngredients();
            DataTable dt = ExecuteQuery("Select * from IngredientsInRecipe;");
            List<Ingredient> ingredientsInRecipe = new List<Ingredient>();
            foreach (DataRow row in dt.Rows)
            {
                ingredientsInRecipe.Add(new Ingredient
                {
                    RecipeId = (int)row["RecipeId"],
                    Id = (int)row["IngredientId"],
                    Amount = (int)row["Amount"],
                    Unit = (Unit)row["Unit"]
                });
            }
            //sætter navnet og type på de individuelle ingredients til hver recipe
            ingredientsInRecipe.ForEach(i => i.Name = ingredients.Find(ing => ing.Id == i.Id).Name);
            ingredientsInRecipe.ForEach(i => i.Type = ingredients.Find(ing => ing.Id == i.Id).Type);
            return ingredientsInRecipe;
        }
        public List<Ingredient> GetAllIngredients()
        {
            DataTable dt = ExecuteQuery("select * from Ingredients;");
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (DataRow row in dt.Rows)
            {
                ingredients.Add(new Ingredient
                {
                    Id = (int)row["Id"],
                    Name = (string)row["Name"],
                    Type = (IngredientType)row["Type"]
                });
            }
            return ingredients;
        }

        public Ingredient GetIngredient(int id)
        {
            DataTable dt = ExecuteQuery($"select * from Ingredients where Id = {id};");
            return new Ingredient
            {
                Id = (int)dt.Rows[0]["Id"],
                Name = (string)dt.Rows[0]["Name"],
                Type = (IngredientType)dt.Rows[0]["Type"]
            };
        }

        public int DeleteIngredient(int id)
        {
            return ExecuteNonQuery($"Delete from Ingredients where Id = {id};");
        }

        public int NewIngredient(Ingredient ing)
        {
            return ExecuteNonQuery($"insert into Ingredients (Name, Type) values ('{ing.Name}',{(int)ing.Type});");
        }

        public int UpdateIngredient(Ingredient ing)
        {
            return ExecuteNonQuery($"update Ingredients set Name = '{ing.Name}', Type = {(int)ing.Type} where Id = {ing.Id};");
        }
    }
}
