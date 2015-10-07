using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace EVARest.Models.DAL.Mapper
{
    public class IdentityRoleMapper: EntityTypeConfiguration<IdentityRole>
    {
        public IdentityRoleMapper()
        {
            this.Property(c => c.Name).HasMaxLength(128).IsRequired();
        }
    }
}