﻿using EVARest.Models.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace EVARest.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Challenge> Challenges { get; set; }

        public DateTime StartedAt { get; set; }

        public byte Children { get; set; }

        public Sex Sex { get; set; }

        public DateTime Birthday { get; set; }

        public bool IsStudent { get; set; }

        public bool IsMarried { get; set; }

        public virtual System.Collections.Generic.ICollection<Dislike> Dislikes { get; set; }

        public virtual ICollection<Badge> Badges { get; set; }

        public int Points { get; set; }

        //public virtual Customer Customer { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager userManager,
            string authenticationType)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }

        public ApplicationUser() : base() {
            Badges = new List<Badge>();
            Challenges = new List<Challenge>();
            Dislikes = new List<Dislike>();
        }

        public void AddChallenge(Challenge challenge) {
            if (Challenges.Any(c => c.Date.Date == DateTime.Today))
                throw new ArgumentException("A challenge has already been chosen for today.");

            Challenges.Add(challenge);
            challenge.Name = $"Challenge {Challenges.Count}";

            if (Challenges.Count == 1)
                StartedAt = DateTime.Now;
        }

        public void HasDoneChallenge(Challenge challenge) {
            if (challenge.Date.Date != DateTime.Today)
                throw new ArgumentException("You cannot do a challenge that is from the past.");
            challenge.Done = true;
            Points += challenge.Earnings;
        }

        public void DeleteChallenges() {
            Challenges.Clear();
        }

        public void DeleteChallenge(Challenge challenge) {
            Challenges.Remove(challenge);
        }
    }
}