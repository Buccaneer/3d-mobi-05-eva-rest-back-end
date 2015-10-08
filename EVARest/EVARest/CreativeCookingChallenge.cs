using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest
{
    public class CreativeCookingChallenge : Challenge
    {
        public IList<Ingredient> Ingredients
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}