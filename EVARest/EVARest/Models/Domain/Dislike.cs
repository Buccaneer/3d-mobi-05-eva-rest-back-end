using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models.Domain
{
    public class Dislike
    {
        public Ingredient Ingredient
        {
            get; set;
        }

        public Reason Reason
        {
            get; set;
        }
    }
}