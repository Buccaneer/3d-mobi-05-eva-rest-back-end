using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest
{
    public abstract class Challenge
    {
        public DateTime Date
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string Name
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public bool Done
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int Earnings
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int ChallengeId
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