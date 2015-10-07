using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using MySql.Data.Entity;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(EVARest.Startup))]
namespace EVARest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            ConfigureAuth(app);
            
        }
    }
}
