﻿using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Web.Filters;

namespace Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "MessagesApi",
                routeTemplate: "api/themes/{themeId}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Filters registration 
            config.Filters.Add(new RequireHttpsAttribute());
            config.Filters.Add(new ValidateModelAttribute());
        }
    }
}