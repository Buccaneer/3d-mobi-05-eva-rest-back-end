﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System;

namespace EVARest.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public IList<Challenge> Challenges
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DateTime StartedAt
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public byte Children
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Sex Sex
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DateTime Birthday
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public bool IsStudent
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public bool IsMarried
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public System.Collections.Generic.IList<EVARest.Dislike> Dislikes
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public IList<Badge> Badges
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int Points
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}