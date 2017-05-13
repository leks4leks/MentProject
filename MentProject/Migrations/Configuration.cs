namespace MentProject.Migrations
{
    using Enums;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<MentProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MentProject.Models.ApplicationDbContext";
        }

        protected override void Seed(MentProject.Models.ApplicationDbContext context)
        {
           
        }
    }
}
