using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

namespace EVARest.Models.Domain
{
    public interface IRestaurantRepository
    {
        IQueryable<Restaurant> Restaurants { get;  }
        ICollection<Point> FindRestaurantByDistance(double longitude, double latitude, double distance);
    }

    public class Point {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int Id { get; set; }

        public double Distance { get; set; }
    }

}