using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using EVARest.Models;

namespace EVARest.Models
{
    public class Feedback
    {
        public TimeSpan TimeSpan
        {
            get; set;
        }

        public ApplicationUser User
        {
            get; set;
        }

        public bool StillVegan
        {
            get; set;
        }

        public DateTime Date
        {
            get; set;
        }

        public string Comment
        {
            get; set;
        }
    }
}