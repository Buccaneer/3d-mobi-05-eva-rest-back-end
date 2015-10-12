using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models.Domain
{
    public class RecipeProperty
    {
        public string Value
        {
            get; set;
        }

        public int PropertyId
        {
            get; set;
        }

        /**
         *   Voorbeelden
         *      Difficulty
         *      CookingTime
         *      NumberOfPeople
         */
        public string Type
        {
            get; set;
        }
    }
}