using System.Data;
using EVARest.Models.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using System.Web.Mvc;
using EVARest.Models.Domain.I18n;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Diagnostics;
using System.IO;

namespace EVARest.Models.DAL {
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class RestContext : IdentityDbContext<ApplicationUser> {
        //server=eu-cdbr-azure-west-c.cloudapp.net;database=evavzwrest;uid=bbe87c16c15f06;password=925a4732
        //server=eu-cdbr-azure-west-c.cloudapp.net;port=3306;database=evavzwrest;uid=bbe87c16c15f06;password=925a4732
        public RestContext() : base(nameOrConnectionString: "server=eu-cdbr-azure-west-c.cloudapp.net;port=3306;database=evavzwrest;uid=bbe87c16c15f06;password=925a4732") {
           // Database.Log = s => File.AppendAllText(@"A:\\logs\sql.log",s);
           
        }


        //public new DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Fact> Facts { get; set; }

        public DbSet<OverrideLanguageSpecification> LanguageSpecifications {get;set;}

        public DbSet<Ingredient> Ingredients { get; set; }

        static RestContext() {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
        }
      

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            //modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            //modelBuilder.Entity<ApplicationUser>().HasKey(k => k.Id);

            modelBuilder.Entity<Badge>().ToTable("Badges");
            modelBuilder.Entity<Badge>().HasKey(k => k.BadgeId);
            //modelBuilder.Entity<Badge>().Property(p => p.Description).IsRequired();
            //modelBuilder.Entity<Badge>().Property(p => p.Name).IsRequired();

            modelBuilder.Entity<Feedback>().ToTable("Feedback");
            modelBuilder.Entity<Feedback>().HasKey(k => k.FeedbackId);

            modelBuilder.Entity<Challenge>().ToTable("Challenges");
            modelBuilder.Entity<Challenge>().HasKey(k => k.ChallengeId);
         //   modelBuilder.Entity<Challenge>().Ignore(c => c.Type);
            //modelBuilder.Entity<Challenge>().Property(p => p.Name).IsRequired();

            modelBuilder.Entity<Fact>().ToTable("Facts");
            modelBuilder.Entity<Fact>().HasKey(k => k.FactId);

            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
            modelBuilder.Entity<Ingredient>().HasKey(k => k.IngredientId);
            modelBuilder.Entity<Ingredient>().Property(i => i.Name).HasMaxLength(100);
            modelBuilder.Entity<Component>().Property(c => c.Postfix).HasMaxLength(256);
            modelBuilder.Entity<Component>().Property(c => c.Prefix).HasMaxLength(256);

            modelBuilder.Entity<Recipe>().ToTable("Recipes");
            modelBuilder.Entity<Recipe>().HasKey(k => k.RecipeId);
            modelBuilder.Entity<Recipe>().HasMany(i => i.Ingredients).WithMany().Map((m) => {
                m.ToTable("RecipesIngredients");
                m.MapLeftKey("Recipe");
                m.MapRightKey("Ingredient");
                });
            modelBuilder.Entity<Recipe>().HasMany(p => p.Properties).WithMany().Map((m) => {
                m.ToTable("RecipesProperties");
                m.MapLeftKey("Recipe");
                m.MapRightKey("Property");
            });

            modelBuilder.Entity<RecipeProperty>().ToTable("rProperties");
            modelBuilder.Entity<RecipeProperty>().HasKey(k => k.PropertyId);

            modelBuilder.Entity<Restaurant>().ToTable("Restaurants");
            modelBuilder.Entity<Restaurant>().HasKey(k => k.RestaurantId);

            modelBuilder.Entity<CreativeCookingChallenge>().HasMany(c => c.Ingredients)
                .WithMany().Map(m => {
                    m.ToTable("cccstoingr");
                    m.MapLeftKey("cccid");
                    m.MapRightKey("ingrid");
                

                });
            var restaurant = modelBuilder.Entity<Restaurant>();
            restaurant.Property(r => r.City).HasMaxLength(64);
            restaurant.Property(r => r.Email).HasMaxLength(255);
            restaurant.Property(r => r.Name).HasMaxLength(48);
            restaurant.Property(r => r.Street).HasMaxLength(120);
            restaurant.Property(r => r.Website).HasMaxLength(512);
            restaurant.Property(r => r.Phone).HasMaxLength(90);

            modelBuilder.Entity<Dislike>().ToTable("Dislikes").HasKey(d => d.DislikeId);
            modelBuilder.Entity<Component>().ToTable("Components").HasKey(c => c.ComponentId);

            modelBuilder.Entity<Challenge>().Ignore(c => c.ExpireDate);
            modelBuilder.Entity<Challenge>().Ignore(c => c.TimeToAccept);

            modelBuilder.Entity<OverrideLanguageSpecification>().ToTable("Languages").HasKey(l => l.LanguageStringId);
            modelBuilder.Entity<OverrideLanguageSpecification>().Property(l => l.Type).HasMaxLength(255);
            modelBuilder.Entity<OverrideLanguageSpecification>().Property(l => l.Language).HasMaxLength(255);
            modelBuilder.Entity<OverrideLanguageSpecification>().Property(l => l.EntityPrimaryKey).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_languages", 1)));
            modelBuilder.Entity<OverrideLanguageSpecification>().Property(l => l.Type).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_languages", 2)));
            modelBuilder.Entity<OverrideLanguageSpecification>().Property(l => l.Language).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_languages", 3)));
        }

        public static RestContext Create() {
            return DependencyResolver.Current.GetService<RestContext>();
        }

        public override int SaveChanges() {
            return base.SaveChanges();
        }
    }
}