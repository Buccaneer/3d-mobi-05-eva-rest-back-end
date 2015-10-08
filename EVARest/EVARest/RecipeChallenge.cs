using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest
{
    public class RecipeChallenge : Challenge
    {
        public Recipe Recipe
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public TargetSubject PreparesFor
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