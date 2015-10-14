using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

namespace EVARest.Models.Domain
{
    public interface IRecipeRepository
    {
        IQueryable<Recipe> Recipes { get;  }

        IQueryable<Recipe> FindRecipesByIngredients(IEnumerable<string> ingredients);
        Recipe FindRecipeById(int id);
        IQueryable<Recipe> FindRecipesWithoutIngredients(IEnumerable<Ingredient> ingredients);
   
        IQueryable<Recipe> FindRecipesByProperty(RecipeProperty property);
        IQueryable<Recipe> FindRecipesByProperties(IEnumerable<string> properties);
    }
}