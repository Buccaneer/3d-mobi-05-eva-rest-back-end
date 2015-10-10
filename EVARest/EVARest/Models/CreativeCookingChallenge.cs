using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace EVARest.Models
{
    public class CreativeCookingChallenge : Challenge
    {
        public virtual ICollection<Ingredient> Ingredients
        {
            get; set;
        }
    }
}