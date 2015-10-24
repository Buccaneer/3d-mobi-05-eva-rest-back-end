using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models.Domain
{
    public class RecipeChallenge : Challenge
    {
        public virtual Recipe Recipe
        {
            get; set;
        }

        public TargetSubject PrepareFor
        {
            get; set;
        }
    }
}