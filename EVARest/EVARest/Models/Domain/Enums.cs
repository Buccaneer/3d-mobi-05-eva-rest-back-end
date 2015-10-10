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

    public enum TargetSubject
    {
        Self,
        Friends,
        LovedOnes
    }
    public enum Difficulty
    {
        Starter,
        Beginner,
        Intermediate,
        Expert
    }

    public enum CookingTime
    {
        Fast,
        Medium,
        Long
    }
}