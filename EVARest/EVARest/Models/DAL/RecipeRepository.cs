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
                return _recipes.Include("Ingredients.Ingredient").Include(r=> r.Properties);
            }
        }

        public Recipe FindRecipeById(int id) {
            return Recipes.FirstOrDefault(r => r.RecipeId == id);
        }


        public IQueryable<Recipe> FindRecipesByIngredients(IEnumerable<string> ingredients) {
            return Recipes.Where(r => r.Ingredients.Any(c => ingredients.Contains(c.Ingredient.Name)));
        }

        public IQueryable<Recipe> FindRecipesByProperties(IEnumerable<string> properties) {
            return Recipes.Where(r => r.Properties.Any(p => properties.Contains(p.Value)));
        }

        public IQueryable<Recipe> FindRecipesByProperty(RecipeProperty property) {
            return Recipes.Where(r => r.Properties.Contains(property));
        }

        public IQueryable<Recipe> FindRecipesWithoutIngredients(IEnumerable<Ingredient> ingredients) {
            var names = ingredients.Where(i => i != null).Select(i => i.Name).ToList();
            return Recipes.Where(r => r.Ingredients.All(c => !names.Contains(c.Ingredient.Name)));
        }
        

        public RecipeRepository(RestContext context) {
            _context = context;
            _recipes = context.Recipes;
        }
    }
}
