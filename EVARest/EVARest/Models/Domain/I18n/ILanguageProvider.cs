using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models.Domain.I18n
{
    public interface ILanguageProvider
    {
        void Register<T>(T obj);
        void Translate( string language);
    }
}