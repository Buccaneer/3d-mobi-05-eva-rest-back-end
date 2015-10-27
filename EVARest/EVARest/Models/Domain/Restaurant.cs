using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models.Domain
{
    public class Restaurant
    {
        public string Name
        {
            get; set;
        }

        public double Longitute
        {
            get; set;
        }

        public double Latitude
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string Website
        {
            get; set;
        }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Street { get; set; }

        public int Postal { get; set; }

        public string City { get; set; }

        public int RestaurantId
        {
            get; set;
        }
    }
}