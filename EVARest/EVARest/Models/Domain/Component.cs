using System;
using System.Web;
using System.ComponentModel;

namespace EVARest.Models.Domain
{
    public class Component
    {
        public int ComponentId { get; set; }

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