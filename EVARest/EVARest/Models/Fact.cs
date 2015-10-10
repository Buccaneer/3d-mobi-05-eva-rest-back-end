using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models
{
    public class Fact
    {
        public string Description
        {
            get;set;
        }

        public int FactId
        {
            get; set;
        }
    }
}