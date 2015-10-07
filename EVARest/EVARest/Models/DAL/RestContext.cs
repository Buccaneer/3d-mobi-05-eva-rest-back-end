using EVARest.Models.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace EVARest.Models.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class RestContext:IdentityDbContext<ApplicationUser>
    {
        public RestContext() : base(nameOrConnectionString: "server=localhost;port=3306;database=evarest;uid=root;password=vulhierjewachtwoordin") { }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static RestContext Create()
        {
            return DependencyResolver.Current.GetService<RestContext>();
        }
    }
}