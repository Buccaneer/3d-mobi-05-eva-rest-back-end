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

        public void Translate<T>(T obj,string language)
        {
            if (obj is Recipe)
                TranslateRecipe(obj as Recipe, language);

            throw new ArgumentException($"Type of {obj.GetType().Name} is not supported.");
        }

        private void TranslateRecipe(Recipe recipe, string language)
        {
            language = language.ToUpper();
            if (language.Equals("NL"))
                return;

            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == recipe.RecipeId && l.Type == "Recipe" && l.Language == language)
                .Select(l => new { Key= l.PropertyKey, Value=l.Content});

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                recipe.Name = name.ToString();

            var description = specs.FirstOrDefault(s => s.Key == "Description");
            if (description != null)
                recipe.Description = description.ToString();

            foreach(Component component in recipe.Ingredients)
            {
                TranslateIngredient(component.Ingredient, language);
            }

            foreach (RecipeProperty prop in recipe.Properties)
            {
                TranslateProperty(prop, language);
            }
        }

        private void TranslateIngredient(Ingredient ingredient, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == ingredient.IngredientId && l.Type == "Ingredient" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var name = specs.FirstOrDefault(s => s.Key == "Name");
            if (name != null)
                ingredient.Name = name.ToString();

            var unit = specs.FirstOrDefault(s => s.Key == "Unit");
            if (unit != null)
                ingredient.Unit = unit.ToString();
        }

        private void TranslateProperty(RecipeProperty prop, string language)
        {
            var specs = _context.LanguageSpecifications
                .Where(l => l.EntityPrimaryKey == prop.PropertyId && l.Type == "RecipeProperty" && l.Language == language)
                .Select(l => new { Key = l.PropertyKey, Value = l.Content });

            var type = specs.FirstOrDefault(s => s.Key == "Type");
            if (type != null)
                prop.Type = type.ToString();

            var value = specs.FirstOrDefault(s => s.Key == "Value");
            if (value != null)
                prop.Value = value.ToString();
        }
    }
}