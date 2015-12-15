using EVARest.Models.Domain;
using EVARest.Models.Domain.I18n;
using EVARest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace EVARest.Controllers {
    [Authorize]
    [RoutePrefix("api/Restaurants")]
    public class RestaurantController : ApiController
        {

        private IRestaurantRepository _restaurantRepository;
        private ILanguageProvider _languageProvider;

        /// <summary>
        /// Gets a restaurant
        /// </summary>
        /// <param name="id">The id of the restaurant.</param>
        /// <returns></returns>
         [CacheOutput(ServerTimeSpan = 81000, ClientTimeSpan = 81000)]
        public IHttpActionResult GetRestaurant(int id) {
            var restaurant = _restaurantRepository.Restaurants.FirstOrDefault(r => r.RestaurantId == id);
            if (restaurant == null)
                return BadRequest($"Restaurant with id {id} was not found.");
            _languageProvider.Register(restaurant);
            _languageProvider.Translate(Language);
            return Ok(restaurant);
        }

        /// <summary>
        /// Fetch all the restaurants within a region.
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="distance">Distance in km</param>
        /// <returns></returns>
        /// 
        [Route("Find")]
        [System.Web.Http.HttpPost]
        public IEnumerable<Point> FindRestaurants(GeoLocation loc) {
            if (!ModelState.IsValid)
                throw new ArgumentException("");
            return _restaurantRepository.FindRestaurantByDistance(loc.Longitude, loc.Latitude, loc.Distance);
        }


        private string Language { get {
                var acceptLanguages = Request.Headers.AcceptLanguage.FirstOrDefault();
                return  acceptLanguages == null ? "nl-BE" : acceptLanguages.Value;
            } }

        public RestaurantController(IRestaurantRepository restaurantRepository, ILanguageProvider languageProvider) {
            _restaurantRepository = restaurantRepository;
            _languageProvider = languageProvider;
        }


    }
}
