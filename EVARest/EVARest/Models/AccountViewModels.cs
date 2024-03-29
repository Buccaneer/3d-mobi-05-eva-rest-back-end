﻿using EVARest.Models.Domain;
using System;
using System.Collections.Generic;

namespace EVARest.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ChallengesDone { get; set; }
        public DateTime BirthDay { get; set; }

        public string Budget { get; set; }

        public string TypeOfVegan { get; set; }
      public IEnumerable<Ingredient> Allergies { get; set; }
        public byte PeopleInFamily { get; internal set; }

        public bool DoneSetup { get; set; }
        public int Points { get; set; }
        public IEnumerable<string> Badges { get; set; }
        public bool HasRequestedChallengeToday { get; internal set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }

    public class SettableUserInfoViewModel {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }

        public string Budget { get; set; }

        public string TypeOfVegan { get; set; }
        public int[] Allergies { get; set; }
        public int? PeopleInFamily { get;  set; }

        public bool? DoneSetup { get; set; }
    }
}
