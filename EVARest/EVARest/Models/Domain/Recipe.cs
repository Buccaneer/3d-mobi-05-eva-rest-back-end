using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

using System.Collections.Generic;

namespace EVARest.Models.Domain
{
    public class Recipe
    {
        public virtual ICollection<Component> Ingredients
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string Image
        {
            get; set;
        }

        public int RecipeId
        {
            get; set; 
        }

        public IList<RecipeProperty> Properties
        {
            get; set;
        }

    }
}