using EVARest.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVARest.Models.DAL {
   public static class ExtentionMethods {

        public static IEnumerable<Recipe> TakeRandom(this IQueryable<Recipe> recipes, int count) {
            if (count <= 0)
                throw new Exception("Cant draw a negative number of recipes");

            int left = count;
            int tries = 0;
            ISet<int> taken = new HashSet<int>();
            Random r = new Random();
            int C = recipes.Max(pr => pr.RecipeId);
            while (left > 0 && tries < count * count) {
                var id = r.Next(C);
                var item = recipes.FirstOrDefault(rr => rr.RecipeId == id);
                if (item != null && !taken.Contains(item.RecipeId)) {
                    taken.Add(item.RecipeId);
                    yield return item;
                    left--;
                }
                tries++;
            }
           

        }
    }
}
