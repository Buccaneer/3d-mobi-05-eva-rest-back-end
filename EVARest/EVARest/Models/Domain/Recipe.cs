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
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int RecipeId
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Difficulty Difficulty
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int NumberPpl
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public IList<RecipeProperty> Properties
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public RecipeProperty Type
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public CookingTime CookingTime
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