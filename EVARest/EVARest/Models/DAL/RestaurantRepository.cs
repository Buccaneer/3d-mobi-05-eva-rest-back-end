using EVARest.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVARest.Models.DAL {
    public class RestaurantRepository : IRestaurantRepository {
        private DbSet<Restaurant> _restaurants;
        private RestContext _context;

        public IQueryable<Restaurant> Restaurants {
            get {
                return _restaurants;
            }

        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="longitude">Degrees</param>
        /// <param name="latitude">Degrees</param>
        /// <param name="distance">Kilometers (straal)</param>
        /// <returns></returns>
        public ICollection<Point> FindRestaurantByDistance(double longitude, double latitude, double distance) {
            var dLat = distance / 111.0; // 1 degree long = average(110.567,111.699) so km = degrees * 111 => degrees = km/ 111 
            var dLong = distance / (111.320 * Math.Cos(dLat));
            /* Conversie
               1degree = (111.320 * cos(dLat)) km : km = ?degree 
               => distance / (111.320 * cos(dLat))
            */
            var square = _restaurants.Where(r => r.Longitute >= (longitude - dLong) && r.Longitute <= (longitude + dLong)
                            && r.Latitude >= (latitude - dLat) && r.Latitude <= (latitude + dLat)).ToList();

            var kmLat = latitude * 111.0;
            var kmLong = longitude * (111.320 * Math.Cos(latitude));

            return square.Select(r => new Point() {
                Latitude = r.Latitude,
                Longitude = r.Longitute,
                Id = r.RestaurantId,
                Distance = Math.Sqrt(Math.Pow((r.Longitute * (111.320 * Math.Cos(r.Latitude))) - kmLong, 2) +
                Math.Pow(r.Latitude * 111 - kmLat, 2))
            }).Where(p => p.Distance <= distance).ToList();
        }

        public RestaurantRepository(RestContext context) {
            _context = context;
            _restaurants = context.Restaurants;
        }
    }
}
