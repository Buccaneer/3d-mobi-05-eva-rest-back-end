using EVARest.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace EVARest.Models.DAL.Mapper
{
    public class ApplicationUserMapper:EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMapper()
        {
            this.ToTable("AspNetUsers").Property(c => c.UserName).HasMaxLength(128).IsRequired();
            HasMany(u => u.Challenges).WithRequired();
        }
    }
}