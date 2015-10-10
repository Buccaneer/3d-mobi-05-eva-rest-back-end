using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models
{
    public class Badge
    {
        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }
    }
}