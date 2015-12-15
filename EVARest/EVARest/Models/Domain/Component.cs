using System;
using System.Web;
using System.ComponentModel;

namespace EVARest.Models.Domain
{
    public class Component
    {
        public int ComponentId { get; set; }

   public string Prefix { get; set; }
        public string Postfix { get; set; }

        public virtual Ingredient Ingredient
        {
            get; set;
        }
    }
}