using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models
{
    public class RecipeChallenge : Challenge
    {
        public Recipe Recipe
        {
            get; set;
        }

        public TargetSubject PreparesFor
        {
            get; set;
        }
    }
}