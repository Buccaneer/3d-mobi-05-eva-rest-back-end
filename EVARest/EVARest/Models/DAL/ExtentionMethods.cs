using EVARest.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVARest.Models.DAL {
    public static class ExtentionMethods {

        public static IEnumerable<Recipe> TakeRandom(this IQueryable<Recipe> recipes, int count, bool fastWay = true) {
            if (count <= 0)
                throw new Exception("Cant draw a negative number of recipes");

            int left = count;
            int tries = 0;
            ISet<int> taken = new HashSet<int>();
            Random r = new Random();
            int C = 0;
            IList<int> possibleIds = null;
            if (!fastWay) {
                possibleIds = recipes.Select(recipe => recipe.RecipeId).ToList();
                C = possibleIds.Count;
                while (left > 0 && tries < count * count) {
                    int id = possibleIds[r.Next(C)];
                    if (!taken.Contains(id)) {
                        left--;
                        taken.Add(id);
                    }
                    tries++;
                }
            } else {
                C = recipes.Max(recipe => recipe.RecipeId);
                while (left > 0 && tries < count * count) {
                    int id = r.Next(C);
                    if (!taken.Contains(id)) {
                        left--;
                        taken.Add(id);
                    }
                    tries++;
                }
            }

            return recipes.Where(recipe => taken.Contains(recipe.RecipeId));


            
        }


    }
}

