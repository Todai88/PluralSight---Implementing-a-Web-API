﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Data.Entity;
using WebApiContrib.Formatting.Jsonp;

namespace CountingKs
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Food",
                routeTemplate: "api/nutrition/foods/{foodid}",
                defaults: new { controller = "foods", foodid = RouteParameter.Optional }
                //constraints: new { id = "/d+" }
            );

            config.Routes.MapHttpRoute(
                name: "Measures",
                routeTemplate: "api/nutrition/foods/{foodid}/measures/{measuresid}",
                defaults: new { controller = "measures", measuresid = RouteParameter.Optional }
                //constraints: new { id = "/d+" }
            );

            config.Routes.MapHttpRoute(
            name: "Diaries",
            routeTemplate: "api/user/diaries/{diaryid}",
            defaults: new { controller = "diaries", diaryid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
            name: "DiarySummary",
            routeTemplate: "api/user/diaries/{diaryid}/summary",
            defaults: new { controller = "diarysummary"}
            );

            config.Routes.MapHttpRoute(
                name: "DiaryEntries",
                routeTemplate: "api/user/diaries/{diaryid}/entries/{id}",
                defaults: new { controller = "diaryentries", id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Remove comments to add JSONP support
            //var formatter = new JsonpMediaTypeFormatter(jsonFormatter);
            //config.Formatters.Insert(0, formatter);

#if !DEBUG
            config.Filters.Add(new RequireHttpsAttribute());
#endif

        }
    }
}