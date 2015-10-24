using EVARest.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVARest.Models.DAL {
    public class RecipeRepository : IRecipeRepository {
        private RestContext _context;
        private DbSet<Recipe> _recipes;

        public IQueryable<Recipe> Recipes {
            get {
                return _recipes;
            }
        }

        public Recipe FindRecipeById(int id) {
            return _recipes.FirstOrDefault(r => r.RecipeId == id);
        }


        public IQueryable<Recipe> FindRecipesByIngredients(IEnumerable<string> ingredients) {
            return _recipes.Where(r => r.Ingredients.Any(c => ingredients.Contains(c.Ingredient.Name)));
        }

        public IQueryable<Recipe> FindRecipesByProperties(IEnumerable<string> properties) {
            return _recipes.Where(r => r.Properties.Any(p => properties.Contains(p.Value)));
        }

        public IQueryable<Recipe> FindRecipesByProperty(RecipeProperty property) {
            return _recipes.Where(r => r.Properties.Contains(property));
        }

        public IQueryable<Recipe> FindRecipesWithoutIngredients(IEnumerable<Ingredient> ingredients) {
            return _recipes.Where(r => r.Ingredients.Any(c => !ingredients.Contains(c.Ingredient)));
        }
        

        public RecipeRepository(RestContext context) {
            _context = context;
            _recipes = context.Recipes;
        }
    }
}
