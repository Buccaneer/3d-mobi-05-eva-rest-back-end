using EVARest.Models.DAL;
using EVARest.Models.Domain;
using EVARest.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EVARest.ViewModels {
    public class ChallengeViewModel {
        [Required(ErrorMessageResourceName ="ChallengeTypeRequired", ErrorMessageResourceType =typeof(Resources))]
        public string Type { get; set; }

        public int[] IngredientsId { get; set; }
        public int RecipeId { get; set; }

        public int RestaurantId { get; set; }

        public ChallengeFactory CreateFactory() {
            try {
                var tt = Type;
                if (Type.Contains(".")) {
                    var t =Type.Split('.');
                    tt = t[1];
                }
                var factoryClassName = "EVARest.ViewModels." + tt + "ChallengeFactory";
                Type type = Assembly.GetAssembly(typeof(ChallengeFactory)).GetType(factoryClassName);
                return (ChallengeFactory)Activator.CreateInstance(type);
            } catch 
            {
                return new TextChallengeFactory();
            }
        }

      
       public ChallengeViewModel() {
            RestaurantId = RecipeId = -1;
        }

    }

    public abstract class ChallengeFactory {
        public Challenge CreateChallenge(RestContext context, ChallengeViewModel cvm ) {
            var challenge = CreateInstance();

            FillGeneralData(challenge, cvm);
            FillSpecificData(challenge, context, cvm);

            return challenge;
        }

        /// <summary>
        /// Fill the challenge with challengespecific data.
        /// </summary>
        /// <param name="challenge">The object to be filled.</param>
        /// <param name="context">Context where you can get data.</param>
        protected abstract void FillSpecificData(Challenge challenge, RestContext context, ChallengeViewModel data);

        private void FillGeneralData(Challenge challenge, ChallengeViewModel data) {
            challenge.Date = DateTime.Now;
            challenge.Done = false;
            challenge.Type = data.Type;
            if (data.Type.Contains(".")) {
                string[] parts = data.Type.Split('.');
                challenge.Type = parts[0];
                data.Type = parts[1];
            }
            

        }

        /// <summary>
        /// Creates an empty challenge object.
        /// </summary>
        /// <returns>According to witch type of ChallengeViewModel an according Challenge type.</returns>
        protected abstract Challenge CreateInstance();
    }

    public class TextChallengeFactory : ChallengeFactory {
        protected override Challenge CreateInstance() {
            return new TextChallenge();
        }

        protected override void FillSpecificData(Challenge challenge, RestContext context, ChallengeViewModel data) {
         
        }
    }

    public class CreativeCookingChallengeFactory : ChallengeFactory {
      
       

        protected override Challenge CreateInstance() {
            return new CreativeCookingChallenge();
        }

        protected override void FillSpecificData(Challenge challenge, RestContext context, ChallengeViewModel data) {
            var c = challenge as CreativeCookingChallenge;

            if (data.IngredientsId == null)
                throw new NullReferenceException("Ingredients are required.");

            foreach (int ingredientId in data.IngredientsId) {
                var ingredient = context.Ingredients.FirstOrDefault(i => i.IngredientId == ingredientId);
                if (ingredient != null)
                    c.Ingredients.Add(ingredient);

            }

            c.Earnings = (int)(1.5 * c.Ingredients.Count);
        }
    }

    public class RecipeChallengeFactory : ChallengeFactory {
        private static Random R = new Random();
       
        
        protected override Challenge CreateInstance() {
            return new RecipeChallenge();
        }

        protected override void FillSpecificData(Challenge challenge, RestContext context, ChallengeViewModel data) {
            var recipe = context.Recipes.FirstOrDefault(r => r.RecipeId == data.RecipeId);
            var c = (challenge as RecipeChallenge);
            if (recipe == null)
                throw new NullReferenceException($"Recipe with id {data.RecipeId} was not found.");

            c.Recipe = recipe;
            c.PrepareFor = (TargetSubject)R.Next(0, 4);
            c.Earnings = 1;
        }
    }

    public class RestaurantChallengeFactory : ChallengeFactory {
        protected override Challenge CreateInstance() {
            return new RestaurantChallenge();
        }

        protected override void FillSpecificData(Challenge challenge, RestContext context, ChallengeViewModel data) {
            var restaurant = context.Restaurants.FirstOrDefault(r => r.RestaurantId == data.RestaurantId);
            var c = (challenge as RestaurantChallenge);
            if (restaurant == null)
                throw new NullReferenceException($"Restaurant with id {data.RestaurantId} was not found.");

            c.Earnings = 3;
            c.Restaurant = restaurant;
         

        }
    }
}
