using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using EVARest.Models.Domain;

namespace EVARest.Models.DAL
{
    public class RestInitializer : DropCreateDatabaseIfModelChanges<RestContext> {
        protected override void Seed(RestContext context)
        {
            try
            {
                
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

     
    }
}