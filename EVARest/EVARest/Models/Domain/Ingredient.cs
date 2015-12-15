using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models.Domain
{
    public class Ingredient
    {
        public string Name
        {
            get; set;
        }

        public int IngredientId
        {
            get;set;
        }
    }
}