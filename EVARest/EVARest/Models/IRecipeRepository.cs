using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

namespace EVARest.Models
{
    public interface IRecipeRepository
    {
        IQueryable<Recipe> Recipes { get; set; }

        IQueryable<Recipe> FindRecipesByIngredients(IEnumerable<Ingredient> ingredients);
        Recipe FindRecipeById(int id);
        IQueryable<Recipe> FindRecipesWithoutIngredients(IEnumerable<Ingredient> ingredients);
        IQueryable<Recipe> FindRecipesByCookingTime(RecipeProperty cookingTime);
        IQueryable<Recipe> FindRecipesByProperty(RecipeProperty property);
        IQueryable<Recipe> FindRecipesByProperties(IEnumerable<RecipeProperty> properties);
    }
}