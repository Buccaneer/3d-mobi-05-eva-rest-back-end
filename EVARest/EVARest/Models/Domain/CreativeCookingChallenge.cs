using System.Collections.Generic;

namespace EVARest.Models.Domain
{
    public class CreativeCookingChallenge : Challenge
    {
        public virtual ICollection<Ingredient> Ingredients
        {
            get; set;
        }

        public virtual Recipe Recipe { get; set; }

        public CreativeCookingChallenge() : base() {
            Ingredients = new List<Ingredient>();
        }
    }
}