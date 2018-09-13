﻿using SSXX.Manage.Web.AllControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SSXX.Manage.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            config.Filters.Add(new TokenProjectorAttribute());

           // config.MessageHandlers.Add(new RequestHandler());
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
