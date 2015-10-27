using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using EVARest.Models.Domain;
using Microsoft.AspNet.Identity;

namespace EVARest.Models.DAL
{
    public class RestInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RestContext>
    {
        protected override void Seed(RestContext context)
        {
            try
            {

                // BADGES
                Badge badge1 = new Badge() { BadgeId = 1, Name = "Week long", Description = "Well done, you've been completing daily challenges for a full week." };
                Badge badge2 = new Badge() { BadgeId = 2, Name = "Two weeks in", Description = "Keep it up! You're now two weeks in the challenge. Only one more week to go." };
                Badge badge3 = new Badge() { BadgeId = 3, Name = "Made it", Description = "Congratulations, you've made it! You completed the 21 day challenge. Feel free to keep completing daily challenges." };
                Badge badge4 = new Badge() { BadgeId = 4, Name = "Month long", Description = "We're so happy to see you continue completing challenges!" };
                Badge badge5 = new Badge() { BadgeId = 5, Name = "A year gone by", Description = "It's been a year since you started completing challenges." };

                
                
                // FACTS
                Fact fact = new Fact() {FactId = 1, Description = "FactDescription"};

                // ADD EVERYTHING TO CONTEXT
                //context.Users.Add(user);
             
                context.Facts.Add(fact);
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string s = "Error while creating database";
                foreach (var eve in e.EntityValidationErrors)
                {
                    s += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.GetValidationResult());
                    foreach (var ve in eve.ValidationErrors)
                    {
                        s += String.Format("- Graad: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(s);
            }
        }

        private ApplicationUser CreateAccount(RestContext context) {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var role = new IdentityRole("admin");
            roleManager.Create(role);

            const string name = "ss@hogent.be";
            const string password = "P@ssword1";
            ApplicationUser admin = new ApplicationUser { UserName = name, Email = name };
            userManager.Create(admin, password);
            userManager.SetLockoutEnabled(admin.Id, false);
            userManager.AddToRole(admin.Id, "admin");
            return admin;

        }
    }
}