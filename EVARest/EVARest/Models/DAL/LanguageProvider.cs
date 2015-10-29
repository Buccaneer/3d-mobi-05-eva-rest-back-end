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
        private RestContext _context;

        public LanguageProvider(RestContext context)
        {
            _context = context;

        }

        public void Translate<T>(T obj, string language)
        {
            language = language.ToUpper();
            if (language.Equals("nl-BE"))
                return;

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
            else if (obj is WorkshopChallenge) //Workshopchalleng empty?
                TranslateWorkshopChallenge(obj as WorkshopChallenge, language);
            else if (obj is RegionRestaurantChallenge) //Also empty
                TranslateRegionRestaurantChallenge(obj as RegionRestaurantChallenge, language);
            else if (obj is RecipeChallenge)
                TranslateRecipeChallenge(obj as RecipeChallenge, language);
            else if (obj is RegionRecipeChallenge)
                TranslateRegionRecipeChallenge(obj as RegionRecipeChallenge, language);
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
            else if (obj is NewsletterChallenge)//Also empty
                TranslateNewsletterChallenge(obj as NewsletterChallenge, language);
            else
                throw new ArgumentException($"Type of {obj.GetType().Name} is not supported.");
        }

        private void TranslateRecipe(Recipe recipe, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == recipe.RecipeId && l.Type == "Recipe" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                recipe.Name = name.Value;

            var description = specs.FirstOrDefault(s => s.Key == "Description");
            if (description != null)
                recipe.Description = description.Value;

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
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == ingredient.IngredientId && l.Type == "Ingredient" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                ingredient.Name = name.Value;

            var unit = specs.FirstOrDefault(s => s.Key == "Unit");
            if (unit != null)
                ingredient.Unit = unit.Value;
        }

        private void TranslateRecipeProperty(RecipeProperty prop, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == prop.PropertyId && l.Type == "RecipeProperty" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var type = specs.FirstOrDefault(s => s.Key == "Type");
            if (type != null)
                prop.Type = type.Value;

            var value = specs.FirstOrDefault(s => s.Key == "Value");
            if (value != null)
                prop.Value = value.Value;
        }

        private void TranslateCCC(CreativeCookingChallenge creativeCookingChallenge, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == creativeCookingChallenge.ChallengeId && l.Type == "CreativeCookingChallenge" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            foreach (Ingredient ingredient in creativeCookingChallenge.Ingredients)
            {
                TranslateIngredient(ingredient, language);
            }

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                creativeCookingChallenge.Name = name.Value;
        }

        private void TranslateRestaurant(Restaurant restaurant, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == restaurant.RestaurantId && l.Type == "Restaurant" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                restaurant.Name = name.Value;

            var description = specs.FirstOrDefault(s => s.Key == "Description");
            if (description != null)
                restaurant.Description = description.Value;

            var website = specs.FirstOrDefault(s => s.Key == "Website");
            if (website != null)
                restaurant.Website = website.Value;
        }

        private void TranslateWorkshopChallenge(WorkshopChallenge workshopChallenge, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == workshopChallenge.ChallengeId && l.Type == "WorkshopChallenge" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                workshopChallenge.Name = name.Value;
        }

        private void TranslateRegionRestaurantChallenge(RegionRestaurantChallenge regionRestaurantChallenge, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == regionRestaurantChallenge.ChallengeId && l.Type == "RegionRestaurantChallenge" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                regionRestaurantChallenge.Name = name.Value;
        }

        private void TranslateRecipeChallenge(RecipeChallenge recipeChallenge, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == recipeChallenge.ChallengeId && l.Type == "RecipeChallenge" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                recipeChallenge.Name = name.Value;

            TranslateRecipe(recipeChallenge.Recipe, language);
        }

        private void TranslateRegionRecipeChallenge(RegionRecipeChallenge regionRecipeChallenge, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == regionRecipeChallenge.ChallengeId && l.Type == "RegionRecipeChallenge" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                regionRecipeChallenge.Name = name.Value;

            TranslateRecipeProperty(regionRecipeChallenge.Region, language);
        }
        private void TranslateDislike(Dislike dislike, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == dislike.DislikeId && l.Type == "Dislike" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            TranslateIngredient(dislike.Ingredient, language);
        }

        private void TranslateApplicationUser(ApplicationUser applicationUser, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey.ToString() == applicationUser.Id && l.Type == "ApplicationUser" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

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
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == badge.BadgeId && l.Type == "Badge" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                badge.Name = name.Value;

            var description = specs.FirstOrDefault(s => s.Key == "Description");
            if (description != null)
                badge.Description = description.Value;
        }

        private void TranslateComponent(Component component, string language)
        {
            TranslateIngredient(component.Ingredient, language);
        }

        private void TranslateRestaurantChallenge(RestaurantChallenge restaurantChallenge, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == restaurantChallenge.ChallengeId && l.Type == "RestaurantChallenge" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                restaurantChallenge.Name = name.Value;

            TranslateRestaurant(restaurantChallenge.Restaurant, language);
        }

        private void TranslateFeedback(Feedback feedback, string language)
        {
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
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == fact.FactId && l.Type == "Fact" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var description = specs.FirstOrDefault(s => s.Key == "Description");
            if (description != null)
                fact.Description = description.Value;

        }

        private void TranslateNewsletterChallenge(NewsletterChallenge newsletterChallenge, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == newsletterChallenge.ChallengeId && l.Type == "NewsletterChallenge" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                newsletterChallenge.Name = name.Value;
        }
    }
}