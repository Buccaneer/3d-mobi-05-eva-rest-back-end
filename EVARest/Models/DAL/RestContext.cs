using System.Data;
using EVARest.Models.Domain;
using MySql.Data.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace EVARest.Models.DAL {
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class RestContext : DbContext {

        //public new DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Fact> Facts { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        static RestContext() {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
        }

        public RestContext() : base(nameOrConnectionString: "server=127.0.0.1;port=3306;database=evarest;uid=root;password=jasperke2") { }

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
            modelBuilder.Entity<Challenge>().Ignore(c => c.Type);
            //modelBuilder.Entity<Challenge>().Property(p => p.Name).IsRequired();

            modelBuilder.Entity<Fact>().ToTable("Facts");
            modelBuilder.Entity<Fact>().HasKey(k => k.FactId);

            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
            modelBuilder.Entity<Ingredient>().HasKey(k => k.IngredientId);

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

            modelBuilder.Entity<Dislike>().ToTable("Dislikes").HasKey(d => d.DislikeId);
            modelBuilder.Entity<Component>().ToTable("Components").HasKey(c => c.ComponentId);
        }

    }
}