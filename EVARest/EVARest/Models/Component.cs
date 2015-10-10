using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models
{
    public class Component
    {

        public double Quantity
        {
            get; set;
        }

        public Ingredient Ingredient
        {
            get; set;
        }
    }
}