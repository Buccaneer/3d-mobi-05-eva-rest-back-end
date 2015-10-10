using System.Security.Claims;
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
        public virtual ICollection<Challenge> Challenges
        {
            get; set;
        }

        public DateTime StartedAt
        {
            get; set;
        }

        public byte Children
        {
            get; set;
        }

        public Sex Sex
        {
            get; set;
        }

        public DateTime Birthday
        {
            get; set;
        }

        public bool IsStudent
        {
            get; set;
        }

        public bool IsMarried
        {
            get; set;
        }

        public virtual System.Collections.Generic.ICollection<Dislike> Dislikes
        {
            get; set;
        }

        public virtual ICollection<Badge> Badges
        {
            get; set;
        }

        public int Points
        {
            get; set;
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