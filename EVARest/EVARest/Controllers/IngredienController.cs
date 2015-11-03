using EVARest.Models.DAL;
using EVARest.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EVARest.Controllers
{
    [Authorize]
    [RoutePrefix("api/Ingredients")]
    public class IngredientController : ApiController
    {

        private RestContext _context;

        [HttpGet]
        public IEnumerable<Ingredient> FindIngredientsStartingBy(string name) {
            return _context.Ingredients.Where(i => i.Name.ToLower().StartsWith(name.ToLower())).OrderBy(s => s.Name);
        }

        public IngredientController(RestContext context) {
            _context = context;
        }
    }
}
