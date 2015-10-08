using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest
{
    public interface IRestaurantRepository
    {
        IQueryable<Restaurant> Restaurants { get; set; }
        ICollection<Restaurant> FindRestaurantByDistance(double longitude, double latitude, double distance);
    }
}