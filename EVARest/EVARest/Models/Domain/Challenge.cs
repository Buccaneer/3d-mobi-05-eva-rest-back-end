using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models.Domain {
    public abstract class Challenge {
        public DateTime Date {
            get; set;
        }

        public string Name {
            get; set;
        }


        public DateTime ExpireDate {
            get {
                return Date.AddHours(24);
            }
        }

        public long TimeToAccept {
            get {
                return (long)(ExpireDate - DateTime.Now).TotalMinutes;
            }
        }
        public bool Done {
            get; set;
        }

        public string Thumbnail { get; set; }

        public int Earnings {
            get; set;
        }

        public int ChallengeId {
            get; set;
        }
        private string _type;

        protected virtual void OnTypeChanged() { }
        public string Type {
            get {
                return _type;
            } set {
                _type = value;
                OnTypeChanged();
            }
        }
    }
}