using EVARest.Models.DAL;
using EVARest.Models.Domain;
using EVARest.Models.Domain.I18n;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace EVARest.Controllers
{
    [Authorize]
    [RoutePrefix("api/Ingredients")]
    public class IngredientController : ApiController
    {

        private RestContext _context;
        private ILanguageProvider _languageProvider;

        [HttpGet]
        [CacheOutput(ServerTimeSpan = 81000, ClientTimeSpan = 81000)]
        public IEnumerable<Ingredient> FindIngredientsStartingBy(string name) {
            return _context.Ingredients.Where(i => i.Name.ToLower().StartsWith(name.ToLower())).OrderBy(s => s.Name);
        }

        private string Language {
            get {
                var acceptLanguages = Request.Headers.AcceptLanguage.FirstOrDefault();
                return acceptLanguages == null ? "nl-BE" : acceptLanguages.Value;
            }
        }

        public IngredientController(RestContext context, ILanguageProvider languageProvider) {
            _context = context;
            _languageProvider = languageProvider;
        }
    }
}
