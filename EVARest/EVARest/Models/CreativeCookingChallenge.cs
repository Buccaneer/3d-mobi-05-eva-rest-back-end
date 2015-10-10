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