using EVARest.Models.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Web;
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
        }

        public static RestContext Create()
        {
            return DependencyResolver.Current.GetService<RestContext>();
        }
    }
}