using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models.Domain
{
    public enum Sex
    {
        Female,
        Male,
        NotShared
    }

    public enum Reason
    {
        Allergy = 1,
        Dislike = 2
    }

    public enum TargetSubject : int
    {
        Self = 0,
        Friends = 1,
        Family = 2,
        LovedOne = 3
    }

}