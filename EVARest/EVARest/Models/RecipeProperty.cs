﻿using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace EVARest.Models
{
    public class RecipeProperty
    {
        public string Description
        {
            get; set;
        }

        public int PropertyId
        {
            get; set;
        }
    }
}