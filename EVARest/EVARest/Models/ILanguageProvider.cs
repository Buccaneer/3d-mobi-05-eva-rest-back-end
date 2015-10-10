using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models
{
    public interface ILanguageProvider
    {
        void TranslateRecipe(Recipe recipe, string language);
    }
}