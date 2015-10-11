using EVARest.Models.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using System.Web.Mvc;

namespace EVARest.Models.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class RestContext : IdentityDbContext<ApplicationUser>
    {
        static RestContext()
        {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
        }

        public RestContext() : base(nameOrConnectionString: "server=eu-cdbr-azure-west-c.cloudapp.net;port=3306;database=evavzwrest;uid=bbe87c16c15f06;password=925a4732") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            
            modelBuilder.Entity<Badge>().ToTable("Badges");
            modelBuilder.Entity<Badge>().HasKey(k => k.BadgeId);
            modelBuilder.Entity<Badge>().Property(p => p.Description).IsRequired();
            modelBuilder.Entity<Badge>().Property(p => p.Name).IsRequired();

            modelBuilder.Entity<Challenge>().ToTable("Challenges");
            modelBuilder.Entity<Challenge>().HasKey(k => k.ChallengeId);
            modelBuilder.Entity<Challenge>().Property(p => p.Name).IsRequired();

            modelBuilder.Entity<Fact>().ToTable("Facts");
            modelBuilder.Entity<Fact>().HasKey(k => k.FactId);

            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
            modelBuilder.Entity<Ingredient>().HasKey(k => k.IngredientId);

            modelBuilder.Entity<Recipe>().ToTable("Recipes");
            modelBuilder.Entity<Recipe>().HasKey(k => k.RecipeId);
            modelBuilder.Entity<Recipe>().HasMany(i => i.Ingredients).WithMany();
            modelBuilder.Entity<Recipe>().HasMany(p => p.Properties).WithMany();

            modelBuilder.Entity<RecipeProperty>().ToTable("RecipeProperties");
            modelBuilder.Entity<RecipeProperty>().HasKey(k => k.PropertyId);

            modelBuilder.Entity<Restaurant>().ToTable("Restaurants");
            modelBuilder.Entity<Restaurant>().HasKey(k => k.RestaurantId);
        }

        public static RestContext Create()
        {
            return DependencyResolver.Current.GetService<RestContext>();
        }
    }
}