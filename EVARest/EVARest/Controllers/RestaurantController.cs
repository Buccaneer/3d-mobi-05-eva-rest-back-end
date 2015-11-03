using EVARest.Models.Domain;
using EVARest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace EVARest.Controllers {
    [Authorize]
    [RoutePrefix("api/Restaurants")]
    public class RestaurantController : ApiController
        {

        private IRestaurantRepository _restaurantRepository;

        /// <summary>
        /// Gets a restaurant
        /// </summary>
        /// <param name="id">The id of the restaurant.</param>
        /// <returns></returns>
        public IHttpActionResult GetRestaurant(int id) {
            var restaurant = _restaurantRepository.Restaurants.FirstOrDefault(r => r.RestaurantId == id);
            if (restaurant == null)
                return BadRequest($"Restaurant with id {id} was not found.");

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


        public RestaurantController(IRestaurantRepository restaurantRepository) {
            _restaurantRepository = restaurantRepository;
        }


    }
}
