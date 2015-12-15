using EVARest.Models.Domain.I18n;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVARest.Models.Domain;

namespace EVARest.Models.DAL
{
    public class LanguageProvider : ILanguageProvider
    {
        private IList<object> _objectStore = new List<object>();
        private class FetchingObject {
            public int PrimaryKey;
            public string Type;
            public string Language;
           

        }
        private class FetchedData {
            public string Column { get; set; }
            public string Content { get; set; }
        }
        private IList<FetchingObject> _toFetch;
        private RestContext _context;

        private IDictionary<string, IDictionary<string,string>> _data = new Dictionary<string, IDictionary<string,string>>();

        public LanguageProvider(RestContext context)
        {
            _context = context;

        }
        private void Begin() {
            _toFetch = new List<FetchingObject>();
            _data.Clear();
            _objectStore.Clear();
        }

        public void Translate(string language) {
            
            language = language.ToUpper();
            if (language.Equals("NL-BE"))
                return;
            FetchAll( language);
            foreach (object obj in _objectStore) {
                try {
                    if (obj is Recipe)
                        TranslateRecipe(obj as Recipe, language);
                    else if (obj is Ingredient)
                        TranslateIngredient(obj as Ingredient, language);
                    else if (obj is RecipeProperty)
                        TranslateRecipeProperty(obj as RecipeProperty, language);
                    else if (obj is CreativeCookingChallenge)
                        TranslateCCC(obj as CreativeCookingChallenge, language);
                    else if (obj is Restaurant)
                        TranslateRestaurant(obj as Restaurant, language);
                    else if (obj is RecipeChallenge)
                        TranslateRecipeChallenge(obj as RecipeChallenge, language);
                    else if (obj is Dislike)
                        TranslateDislike(obj as Dislike, language);
                    else if (obj is ApplicationUser)
                        TranslateApplicationUser(obj as ApplicationUser, language);
                    else if (obj is Badge)
                        TranslateBadge(obj as Badge, language);
                    else if (obj is Component)
                        TranslateComponent(obj as Component, language);
                    else if (obj is RestaurantChallenge)
                        TranslateRestaurantChallenge(obj as RestaurantChallenge, language);
                    else if (obj is Feedback)
                        TranslateFeedback(obj as Feedback, language);
                    else if (obj is Fact)
                        TranslateFact(obj as Fact, language);
                    else
                        throw new ArgumentException($"Type of {obj.GetType().Name} is not supported.");
                } catch { }
            }
            Begin();
        }
        public void Register<T>(T obj)
        {


            if (_toFetch == null)
                Begin();

            if (obj is Recipe)
                AskForRecipe(obj as Recipe);
            else if (obj is Ingredient)
                AskForIngredient(obj as Ingredient);
            else if (obj is RecipeProperty)
                AskForRecipeProperty(obj as RecipeProperty);
            else if (obj is CreativeCookingChallenge)
                AskForCCC(obj as CreativeCookingChallenge);
            else if (obj is Restaurant)
                AskForRestaurant(obj as Restaurant);
            else if (obj is RecipeChallenge)
                AskForRecipeChallenge(obj as RecipeChallenge);
            else if (obj is Dislike)
                AskForDislike(obj as Dislike);
            else if (obj is ApplicationUser)
                AskForApplicationUser(obj as ApplicationUser);
            else if (obj is Badge)
                AskForBadge(obj as Badge);
            else if (obj is Component)
                AskForComponent(obj as Component);
            else if (obj is RestaurantChallenge)
                AskForRestaurantChallenge(obj as RestaurantChallenge);
            else if (obj is Feedback)
                AskForFeedback(obj as Feedback);
            else if (obj is Fact)
                AskForFact(obj as Fact);

          


        }

        private void AskForFact(Fact fact) {
          //  throw new NotImplementedException();
        }

        private void AskForFeedback(Feedback feedback) {
          //  throw new NotImplementedException();
        }

        private void AskForRestaurantChallenge(RestaurantChallenge restaurantChallenge) {
            if (restaurantChallenge == null)
                return;
            _toFetch.Add(new FetchingObject() {
                PrimaryKey = restaurantChallenge.ChallengeId,
                Type = "RestaurantChallenge"
            });

            AskForRestaurant(restaurantChallenge.Restaurant);
        }

        private void AskForComponent(Component component) {
            if (component == null)
                return;
            AskForIngredient(component.Ingredient);
        }

        private void AskForBadge(Badge badge) {
            if (badge == null)
                return;
            _toFetch.Add(new FetchingObject() {
                PrimaryKey = badge.BadgeId,
                Type = "Badge"
            });
        }

        private void AskForApplicationUser(ApplicationUser applicationUser) {
            if (applicationUser == null)
                return;
            foreach (var dislike in applicationUser.Dislikes)
                AskForDislike(dislike);
            foreach (var challenge in applicationUser.Challenges)
                if (challenge is RecipeChallenge)
                    AskForRecipeChallenge(challenge as RecipeChallenge);
                else if (challenge is RestaurantChallenge)
                    AskForRestaurantChallenge(challenge as RestaurantChallenge);
                else if (challenge is CreativeCookingChallenge)
                    AskForCCC(challenge as CreativeCookingChallenge);
        }

        private void AskForDislike(Dislike dislike) {
            if (dislike == null)
                return;
            //_toFetch.Add(new FetchingObject() {
            //    Language = language,
            //    PrimaryKey = dislike.DislikeId,
            //    Type = "Dislike"
            //});
            AskForIngredient(dislike.Ingredient);

        }

        private void AskForRecipeChallenge(RecipeChallenge recipeChallenge) {
            if (recipeChallenge == null)
                return;
            _toFetch.Add(new FetchingObject() {
                PrimaryKey = recipeChallenge.ChallengeId,
                Type = "RecipeChallenge"
            });

            AskForRecipe(recipeChallenge.Recipe);
        }

        private void AskForRestaurant(Restaurant restaurant) {
            if (restaurant == null)
                return;
            _toFetch.Add(new FetchingObject() {
                PrimaryKey = restaurant.RestaurantId,
                Type = "Restaurant"
            });
        }

        private void AskForCCC(CreativeCookingChallenge creativeCookingChallenge) {
            if (creativeCookingChallenge == null)
                return;
            _toFetch.Add(new FetchingObject() {
                PrimaryKey = creativeCookingChallenge.ChallengeId,
                Type = "CreativeCookingChallenge"
            });

            AskForRecipe(creativeCookingChallenge.Recipe);
            foreach (var ingredient in creativeCookingChallenge.Ingredients)
                AskForIngredient(ingredient);
        }

        private void FetchAll(string language) {
            _data.Clear();
            var ids = _toFetch.Select(t => t.PrimaryKey).Distinct();
            var types = _toFetch.Select(t => t.Type).Distinct();
            var data = _context.LanguageSpecifications
                .Where(l => ids.Contains(l.EntityPrimaryKey) 
                && types.Contains(l.Type) 
                && l.Language == language);

            foreach (var id in ids)
                foreach (var type in types)
                    _data.Add($"{id}{type}", new Dictionary<string, string>());

            foreach (var result in data) {
                var key = $"{result.EntityPrimaryKey}{result.Type}";
                if (!_data.ContainsKey(key)) 
                    _data.Add(key, new Dictionary<string, string>());
                
                _data[key].Add(result.PropertyKey, result.Content);
            }
        }

        private void AskForRecipe(Recipe recipe) {
            if (recipe == null)
                return;
            _toFetch.Add(new FetchingObject() { PrimaryKey = recipe.RecipeId, Type = "Recipe" });

            foreach (var component in recipe.Ingredients) {
                AskForIngredient(component.Ingredient);
            }

            foreach (var prop in recipe.Properties)
                AskForRecipeProperty(prop);
        }

        private void AskForRecipeProperty(RecipeProperty prop) {
            if (prop == null)
                return;
            _toFetch.Add(new FetchingObject() {
                PrimaryKey = prop.PropertyId,
                Type = "RecipeProperty"
            });
        }

        private void AskForIngredient(Ingredient ingredient) {
            if (ingredient == null)
                return;
            _toFetch.Add(new FetchingObject() {
                PrimaryKey = ingredient.IngredientId,
                Type = "Ingredient"
            });
        }

        private void TranslateRecipe(Recipe recipe, string language)
        {
            if (recipe == null)
                return;
            var specs = _data[$"{recipe.RecipeId}Recipe"];
            if (specs.ContainsKey("Name"))
                recipe.Name = specs["Name"];

            if (specs.ContainsKey("Description"))
                recipe.Description = specs["Description"];

            foreach (Component component in recipe.Ingredients)
            {
                TranslateIngredient(component.Ingredient, language);
            }

            foreach (RecipeProperty prop in recipe.Properties)
            {
                TranslateRecipeProperty(prop, language);
            }
        }

        private void TranslateIngredient(Ingredient ingredient, string language)
        {
            if (ingredient == null)
                return;
            var specs =  _data[$"{ingredient.IngredientId}Ingredient"];
            if (specs.ContainsKey("Name"))
                ingredient.Name = specs["Name"];

           
        }

        private void TranslateRecipeProperty(RecipeProperty prop, string language)
        {
            if (prop == null)
                return;
            var specs = _data[$"{prop.PropertyId}RecipeProperty"];

            if (specs.ContainsKey("Type"))
                prop.Type = specs["Type"];

            if (specs.ContainsKey("Value"))
                prop.Value = specs["Value"];
        }

        private void TranslateCCC(CreativeCookingChallenge creativeCookingChallenge, string language)
        {
            if (creativeCookingChallenge == null)
                return;
            var specs = _data[$"{creativeCookingChallenge.ChallengeId}CreativeCookingChallenge"];

            foreach (Ingredient ingredient in creativeCookingChallenge.Ingredients)
            {
                TranslateIngredient(ingredient, language);
            }

            if (specs.ContainsKey("Name"))
                creativeCookingChallenge.Name = specs["Name"];
        }

        private void TranslateRestaurant(Restaurant restaurant, string language)
        {
            if (restaurant == null)
                return;
            var specs = _data[$"{restaurant.RestaurantId}Restaurant"];

            if (specs.ContainsKey("Name"))
                restaurant.Name = specs["Name"];

            if (specs.ContainsKey("Description"))
                restaurant.Description = specs["Description"];

            if (specs.ContainsKey("Website"))
                restaurant.Website = specs["Website"];
        }

        private void TranslateWorkshopChallenge(WorkshopChallenge workshopChallenge, string language)
        {
            if (workshopChallenge == null)
                return;
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == workshopChallenge.ChallengeId && l.Type == "WorkshopChallenge" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                workshopChallenge.Name = name.Value;
        }

        private void TranslateRegionRestaurantChallenge(RegionRestaurantChallenge regionRestaurantChallenge, string language)
        {
            if (regionRestaurantChallenge == null)
                return;
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == regionRestaurantChallenge.ChallengeId && l.Type == "RegionRestaurantChallenge" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                regionRestaurantChallenge.Name = name.Value;
        }

        private void TranslateRecipeChallenge(RecipeChallenge recipeChallenge, string language)
        {
            if (recipeChallenge == null)
                return;
            var specs = _data[$"{recipeChallenge.ChallengeId}RecipeChallenge"];

            if (specs.ContainsKey("Name"))
                recipeChallenge.Name = specs["Name"];

            TranslateRecipe(recipeChallenge.Recipe, language);
        }

        private void TranslateRegionRecipeChallenge(RegionRecipeChallenge regionRecipeChallenge, string language)
        {
            if (regionRecipeChallenge == null)
                return;
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == regionRecipeChallenge.ChallengeId && l.Type == "RegionRecipeChallenge" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                regionRecipeChallenge.Name = name.Value;
            
        }
        private void TranslateDislike(Dislike dislike, string language)
        {
            if (dislike == null)
                return;
            TranslateIngredient(dislike.Ingredient, language);
        }

        private void TranslateApplicationUser(ApplicationUser applicationUser, string language)
        {
            if (applicationUser == null)
                return;

            foreach (Challenge challenge in applicationUser.Challenges)
            {
                if (challenge is CreativeCookingChallenge)
                    TranslateCCC(challenge as CreativeCookingChallenge, language);
                if (challenge is NewsletterChallenge)
                    TranslateNewsletterChallenge(challenge as NewsletterChallenge, language);
                if (challenge is RecipeChallenge)
                    TranslateRecipeChallenge(challenge as RecipeChallenge, language);
                if (challenge is RegionRecipeChallenge)
                    TranslateRegionRecipeChallenge(challenge as RegionRecipeChallenge, language);
                if (challenge is RegionRestaurantChallenge)
                    TranslateRegionRestaurantChallenge(challenge as RegionRestaurantChallenge, language);
                if (challenge is RestaurantChallenge)
                    TranslateRestaurantChallenge(challenge as RestaurantChallenge, language);
                if (challenge is WorkshopChallenge)
                    TranslateWorkshopChallenge(challenge as WorkshopChallenge, language);
            }
        }

        private void TranslateBadge(Badge badge, string language)
        {
            if (badge == null)
                return;
            var specs = _data[$"{badge.BadgeId}Badge"];

            if (specs.ContainsKey("Name"))
                badge.Name = specs["Name"];

            if (specs.ContainsKey("Description"))
                badge.Description = specs["Description"];
        }

        private void TranslateComponent(Component component, string language)
        {
            if (component == null)
                return;
            TranslateIngredient(component.Ingredient, language);
        }

        private void TranslateRestaurantChallenge(RestaurantChallenge restaurantChallenge, string language)
        {
            if (restaurantChallenge == null)
                return;
            var specs = _data[$"{restaurantChallenge.ChallengeId}RestaurantChallenge"];

            if (specs.ContainsKey("Name"))
                restaurantChallenge.Name = specs["Name"];

            TranslateRestaurant(restaurantChallenge.Restaurant, language);
        }

        private void TranslateFeedback(Feedback feedback, string language)
        {
            if (feedback == null)
                return;
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == feedback.FeedbackId && l.Type == "Feedback" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var comment = specs.FirstOrDefault(s => s.Key == "Comment");
            if (comment != null)
                feedback.Comment = comment.Value;

            TranslateApplicationUser(feedback.User, language);
        }

        private void TranslateFact(Fact fact, string language)
        {
            if (fact == null)
                return;
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == fact.FactId && l.Type == "Fact" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var description = specs.FirstOrDefault(s => s.Key == "Description");
            if (description != null)
                fact.Description = description.Value;

        }

        private void TranslateNewsletterChallenge(NewsletterChallenge newsletterChallenge, string language)
        {
            if (newsletterChallenge == null)
                return;
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == newsletterChallenge.ChallengeId && l.Type == "NewsletterChallenge" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                newsletterChallenge.Name = name.Value;
        }
    }
}