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

            int left = recipes.Count();
            int c = left;
            int taken = 0;
            var r = new Random();
            Func<int,bool> canTake = (l) => r.NextDouble() <= (double)l / (double)c;

            foreach (var recipe in recipes) {
                if (canTake(left - taken)) {
                    yield return recipe;
                    taken++;
                }
                left--;
            }

        }
    }
}
