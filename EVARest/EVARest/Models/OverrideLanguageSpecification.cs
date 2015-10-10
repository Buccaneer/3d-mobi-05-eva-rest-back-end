using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models
{
    public class OverrideLanguageSpecification
    {
        public int LanguageStringId
        {
            get; set;
        }

        public int EntityPrimaryKey
        {
            get; set;
        }

        public string Language
        {
            get; set;
        }

        public string Content
        {
            get; set;
        }

        public string PropertyKey
        {
            get; set;
        }
    }
}