using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MentProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();
            
            routes.MapRoute(
              name: "GetUsers",
              url: "users",
              defaults: new { controller = "User", action = "Index" },
              namespaces: new[] { "MentProject.Controllers" }
            );

            routes.MapRoute(
              name: "GetUserById",
              url: "users/{id}",
              defaults: new { controller = "User", action = "GetUserById", id = UrlParameter.Optional },
              constraints: new { id = @"\d*" },
              namespaces: new[] { "MentProject.Controllers" }
            );

            routes.MapRoute(
                name: "GetUserByName",
                url: "users/{userName}",
                defaults: new { controller = "User", action = "GetUserByName", userName = UrlParameter.Optional },
                namespaces: new[] { "MentProject.Controllers" }
            );

            routes.MapRoute(
              name: "GetUserForEdit",
              url: "users/{id}/edit",
              defaults: new { controller = "User", action = "Edit", id = UrlParameter.Optional },
              constraints: new { id = @"\d*" },
              namespaces: new[] { "MentProject.Controllers" }
            );

            routes.MapRoute(
             name: "GetUserForDelete",
             url: "users/{id}/delete",
             defaults: new { controller = "User", action = "Delete", id = UrlParameter.Optional },
             constraints: new { id = @"\d*" },
             namespaces: new[] { "MentProject.Controllers" }
           );

            routes.MapRoute(
               name: "CreateUser",
               url: "create-user",
               defaults: new { controller = "User", action = "Create" },
               namespaces: new[] { "MentProject.Controllers" }
            );
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "LoginFromCache", id = UrlParameter.Optional }
            );

        }
    }
}
