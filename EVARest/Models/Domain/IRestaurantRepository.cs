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
        IQueryable<Restaurant> Restaurants { get; set; }
        ICollection<Restaurant> FindRestaurantByDistance(double longitude, double latitude, double distance);
    }
}