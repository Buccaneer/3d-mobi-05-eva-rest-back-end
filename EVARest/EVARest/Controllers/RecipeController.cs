﻿using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EVARest.Models.Domain;
using EVARest.Models.DAL;
using System.Linq;
using EVARest.ViewModels;
using EVARest.Models.Domain.I18n;
using EVARest.App_Start;
using WebApi.OutputCache.V2;

namespace EVARest.Controllers
{
    /// <summary>
    /// Recipe resource
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Recipes")]
   
    public class RecipeController : ApiController
    {
        private IRecipeRepository _recipeRepository;
        private RestContext _context;
        private ApplicationUser _user;
        private ILanguageProvider _languageProvider;

        public RecipeController(IRecipeRepository recipeRepository, ILanguageProvider languageProvider, RestContext context)
        {
            _languageProvider = languageProvider;
            _recipeRepository = recipeRepository;
            _context = context;
        }

        /// <summary>
        /// Returns a max of 50 random recipes that doesn't use recipes in user dislike.
        /// </summary>
        /// <returns>Recipes</returns>
        [Route("")]
     [CacheOutput(ServerTimeSpan =15*60, ClientTimeSpan =15 * 60)]
        public IEnumerable<Recipe> GetAllRecipes() {
            
            var user = User;
            var ingredients = user.Dislikes.Select(s => s.Ingredient);

            var acceptLanguages = Request.Headers.AcceptLanguage.FirstOrDefault();
            string language = acceptLanguages == null ? "nl-BE" : acceptLanguages.Value;

            IEnumerable<Recipe> recipes = _recipeRepository
                .FindRecipesWithoutIngredients(ingredients)
                .TakeRandom(5)
                .ToList();

            recipes.ToList().ForEach(r => _languageProvider.Register(r));

            _languageProvider.Translate(language);
            return recipes.ToList() ;
        }
        /// <summary>
        /// Gives a recipe according to its id
        /// </summary>
        /// <param name="id">the id of the recipe</param>
        /// <returns>A recipe</returns>
        public IHttpActionResult GetRecipe(int id) {
            var recipe = _recipeRepository.FindRecipeById(id);
            if (recipe == null) {
                return NotFound();
            }

            var acceptLanguages = Request.Headers.AcceptLanguage.FirstOrDefault();
            string language = acceptLanguages == null ? "nl-BE" : acceptLanguages.Value;

            _languageProvider.Register(recipe);
            _languageProvider.Translate(language);
            return Ok(recipe);
        }

        /// <summary>
        /// Gives all the recipes with matching properties which the user does not disike.
        /// </summary>
        /// <param name="lsvm">A collection of properties.</param>
        /// <returns>Recipes</returns>
        [Route("ByProperty")]

        [CacheOutput(ServerTimeSpan = 15 * 60, ClientTimeSpan = 15 * 60)]
        [System.Web.Http.HttpPost]
        public IHttpActionResult ByProperty([FromBody]ListOfStringViewModel lsvm) {
            if (lsvm == null || !ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var user = User;
            var ingredients = user.Dislikes.Select(s => s.Ingredient);

            var acceptLanguages = Request.Headers.AcceptLanguage.FirstOrDefault();
            string language = acceptLanguages == null ? "nl-BE" : acceptLanguages.Value;

          

            var recipes = _recipeRepository
               .FindRecipesWithoutIngredients(ingredients)
                .FindRecipesByProperties(lsvm.Values)

                .TakeRandom(5,false)
                .ToList();

            recipes.ForEach(r => _languageProvider.Register(r));
            _languageProvider.Translate(language);
            //var ingredients = user.Dislikes.Select(s => s.Ingredient);
            return Ok(recipes.ToList());
                
        }

        /// <summary>
        /// Gives all the recipes with matching ingredients which the user does not dislike.
        /// </summary>
        /// <param name="lsvm">A collection of ingredientnames</param>
        /// <returns>Recipes</returns>
        [Route("ByIngredient")]

        [CacheOutput(ServerTimeSpan = 15 * 60, ClientTimeSpan = 15 * 60)]
        [System.Web.Http.HttpPost]
        public IHttpActionResult ByIngredient([FromBody]ListOfStringViewModel lsvm) {
            if (lsvm == null || !ModelState.IsValid) {
                return BadRequest("Requires a collection of names.");
            }
            var user = User;
            var ingredients = user.Dislikes.Select(s => s.Ingredient);

            var acceptLanguages = Request.Headers.AcceptLanguage.FirstOrDefault();
            string language = acceptLanguages == null ? "nl-BE" : acceptLanguages.Value;

            var recipes = _recipeRepository
                .FindRecipesWithoutIngredients(ingredients)
                .FindRecipesByIngredients(lsvm.Values)
                .TakeRandom(5,false)
                .ToList();

            recipes.ForEach(r => _languageProvider.Register(r));
            _languageProvider.Translate(language);
  
          //  var ingredients = user.Dislikes.Select(s => s.Ingredient);
            return Ok(recipes.ToList());
        }

        private ApplicationUser User {
            get {
                if (_user != null)
                    return _user;
                var username = RequestContext.Principal.Identity.Name;
                var user = _context.Users
                    .FirstOrDefault(u => u.UserName == username);
                  
                _user = user;
                return user;
            }
        }
    }
}
