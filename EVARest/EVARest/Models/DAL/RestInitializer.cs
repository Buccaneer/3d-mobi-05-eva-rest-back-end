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
                //Badge badge = new Badge() {BadgeId = 1, Name = "NameBadge", Description = "DescriptionBadge"};
                //ApplicationUser user = CreateAccount(context);
                //user.Badges.Add(badge);
                //Feedback feedback = new Feedback()
                //{
                //    FeedbackId = 1,
                //    Comment = "CommentFeedback",
                //    Date = new DateTime(2015, 10, 12),
                //    StillVegan = true,
                //    TimeSpan = new TimeSpan(21, 0, 0, 0),
                //    User = user
                //};
                //Restaurant restaurant = new Restaurant()
                //{
                //    RestaurantId = 1,
                //    Description = "RestaurantDescription",
                //    Latitude = 0.0,
                //    Longitute = 0.0,
                //    Name = "RestaurantName",
                //    Website = "http://www.google.be"
                //};
                //Ingredient ingredient = new Ingredient() {IngredientId = 1, Name = "IngredientName", Unit = "Kilo"};
                //Component component = new Component() {Ingredient = ingredient, Quantity = 1.0};
                //RecipeProperty property = new RecipeProperty()
                //{
                //    PropertyId = 1,
                //    Value = "PropertyValue",
                //    Type = "PropertyType"
                //};
                //Recipe recipe = new Recipe()
                //{
                //    RecipeId = 1,
                //    Description = "RecipeDescription",
                //    Image = "ImageURL",
                //    Ingredients = {component},
                //    Name = "RecipeName",
                //    Properties = {property}
                //};
                //Challenge cookingChallenge = new CreativeCookingChallenge()
                //{
                //    ChallengeId = 1,
                //    Date = new DateTime(2015, 10, 12),
                //    Done = true,
                //    Ingredients = {ingredient},
                //    Earnings = 5,
                //    Name = "ChallengeName"
                //};
                //Fact fact = new Fact() {FactId = 1, Description = "FactDescription"};
                ////    context.Users.Add(user);
                //context.Ingredients.Add(ingredient);
                //user.Challenges.Add(cookingChallenge);
                //context.Restaurants.Add(restaurant);
                //context.Recipes.Add(recipe);
                //context.Feedbacks.Add(feedback);
                //context.Facts.Add(fact);
                //context.SaveChanges();
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