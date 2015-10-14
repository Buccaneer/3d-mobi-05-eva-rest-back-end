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
    public class RestInitializer : System.Data.Entity.DropCreateDatabaseAlways<RestContext>
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

                // RESTAURANTS
                Restaurant restaurant1 = new Restaurant()
                {
                    RestaurantId = 1,
                    Description = "Better than The Fat Duck.",
                    Latitude = 51.0,
                    Longitute = 44.0,
                    Name = "The Vegan Duck",
                    Website = "http://www.google.be"
                };
                Restaurant restaurant2 = new Restaurant()
                {
                    RestaurantId = 2,
                    Description = "Delicious dishes.",
                    Latitude = 51.0,
                    Longitute = 44.0,
                    Name = "Quomodo",
                    Website = "http://www.google.be"
                };
                Restaurant restaurant3 = new Restaurant()
                {
                    RestaurantId = 3,
                    Description = "This bar serves 100% vegetal beer.",
                    Latitude = 51.0,
                    Longitute = 44.0,
                    Name = "The World's End",
                    Website = "http://www.google.be"
                };
                Restaurant restaurant4 = new Restaurant()
                {
                    RestaurantId = 4,
                    Description = "We serve delicious fruity cocktails and soy milkshakes.",
                    Latitude = 51.0,
                    Longitute = 44.0,
                    Name = "Raspberry",
                    Website = "http://www.google.be"
                };
                Restaurant restaurant5 = new Restaurant()
                {
                    RestaurantId = 5,
                    Description = "All sorts of vegetal pie. Do not ask for meat pie.",
                    Latitude = 51.0,
                    Longitute = 44.0,
                    Name = "Pi",
                    Website = "http://www.google.be"
                };

                // INGREDIENTS
                Ingredient kikkererwt = new Ingredient() { IngredientId = 1, Name = "Kikkererwt", Unit = "Blik" };
                Ingredient citroensap = new Ingredient() {IngredientId = 2, Name = "Citroensap", Unit = "Eetlepel"};
                Ingredient knoflook = new Ingredient() {IngredientId = 3, Name = "Knoflook", Unit = "Teen"};
                Ingredient komijnpoeder = new Ingredient() {IngredientId = 4, Name = "Komijnpoeder", Unit = "Theelepel"};
                Ingredient tahin = new Ingredient() {IngredientId = 5, Name = "Tahin", Unit = "Eetlepel"};
                Ingredient olijfolie = new Ingredient() {IngredientId = 6, Name = "Olijfolie", Unit = "Eetlepel"};
                Ingredient zout = new Ingredient() { IngredientId = 7, Name = "Zout", Unit = "" };
                Ingredient peper = new Ingredient() {IngredientId = 8, Name = "Peper", Unit = ""};

                // COMPONENTS
                Component hummusKikkererwt = new Component() { Ingredient = kikkererwt, Quantity = 1.0 };
                Component hummusCitroensap = new Component() { Ingredient = citroensap, Quantity = 1.0 };
                Component hummusKnoflook = new Component() { Ingredient = knoflook, Quantity = 1.0 };
                Component hummusKomijnpoeder = new Component() { Ingredient = komijnpoeder, Quantity = 1.0 };
                Component hummusTahin = new Component() { Ingredient = tahin, Quantity = 2.0 };
                Component hummusOlijfolie = new Component() { Ingredient = olijfolie, Quantity = 3.0 };
                Component hummusZout = new Component() { Ingredient = zout, Quantity = 1.0 };
                Component hummusPeper = new Component() { Ingredient = peper, Quantity = 1.0 };

                // RECIPE PROPERTIES
                RecipeProperty hummusProperty1 = new RecipeProperty()
                {
                    PropertyId = 1,
                    Value = "Makkelijk",
                    Type = "Moeilijkheid"
                };
                RecipeProperty hummusProperty2 = new RecipeProperty()
                {
                    PropertyId = 2,
                    Value = "15 min",
                    Type = "Tijd"
                };
                RecipeProperty hummusProperty3 = new RecipeProperty()
                {
                    PropertyId = 3,
                    Value = "15 min",
                    Type = "Aantal Personen"
                };

                // RECIPES
                Recipe hummus = new Recipe()
                {
                    RecipeId = 1,
                    Description = "Mix alles goed samen met een staafmixer en voeg eventueel nog wat extra water of olijfolie toe tot je een smeuig beleg krijgt.\nHummus is zeer lekker als broodbeleg.\nGebruik je gedroogde kikkererwten, zet dan 150 g kikkererwten een nachtje in de week en kook ze tot ze gaar zijn.",
                    Image = "http://www.evavzw.be/sites/default/files/styles/wieni_gallery_photo/public/recipe/gallery/Hummus-02.jpg?itok=x49ueoqr",
                    Ingredients = { hummusKikkererwt, hummusCitroensap, hummusKnoflook, hummusKomijnpoeder, hummusTahin, hummusOlijfolie, hummusZout, hummusPeper },
                    Name = "Hummus",
                    Properties = { hummusProperty1, hummusProperty2, hummusProperty3 }
                };

                // CHALLENGES
                Challenge cookingChallenge = new CreativeCookingChallenge()
                {
                    ChallengeId = 1,
                    Date = new DateTime(2015, 10, 12),
                    Done = true,
                    Ingredients = { kikkererwt },
                    Earnings = 5,
                    Name = "Kook met kikkererwten"
                };
                Challenge recipeChallenge = new RecipeChallenge()
                {
                    ChallengeId = 2,
                    Date = new DateTime(2015, 10, 13),
                    Done = true,
                    Recipe = hummus,
                    Earnings = 5,
                    Name = "Maak hummus"
                };

                // DISLIKES
                Dislike dislike = new Dislike()
                {
                    DislikeId = 1,
                    Ingredient = new Ingredient() {IngredientId = 9999, Name = "Uranium", Unit = "Vat"},
                    Reason = Reason.Allergy
                };

                // USERS
                ApplicationUser user = CreateAccount(context);
                user.IsMarried = true;
                user.IsStudent = false;
                user.Birthday = new DateTime(1983, 8, 21);
                user.Challenges.Add(cookingChallenge);
                user.Challenges.Add(recipeChallenge);
                user.Badges.Add(badge1);
                user.Badges.Add(badge2);
                user.Badges.Add(badge3);
                user.Badges.Add(badge4);
                user.Badges.Add(badge5);
                user.Children = 2;
                user.Dislikes.Add(dislike);
                user.Points = 9999;
                user.StartedAt = new DateTime(2015, 10, 12);

                // FEEDBACKS
                Feedback feedback = new Feedback()
                {
                    FeedbackId = 1,
                    Comment = "CommentFeedback",
                    Date = new DateTime(2015, 10, 12),
                    StillVegan = true,
                    TimeSpan = new TimeSpan(21, 0, 0, 0),
                    User = user
                };
                
                // FACTS
                Fact fact = new Fact() {FactId = 1, Description = "FactDescription"};

                // ADD EVERYTHING TO CONTEXT
                //context.Users.Add(user);
                context.Challenges.Add(cookingChallenge);
                context.Challenges.Add(recipeChallenge);
                context.Restaurants.Add(restaurant1);
                context.Restaurants.Add(restaurant2);
                context.Restaurants.Add(restaurant3);
                context.Restaurants.Add(restaurant4);
                context.Restaurants.Add(restaurant5);
                context.Recipes.Add(hummus);
                context.Feedbacks.Add(feedback);
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