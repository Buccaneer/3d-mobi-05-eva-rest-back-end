using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EVARest.Models.DAL
{
    public class RestInitializer : System.Data.Entity.NullDatabaseInitializer<RestContext>
    {

    }
}