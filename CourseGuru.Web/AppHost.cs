﻿using System.Collections.Generic;
using System.Net;
using CodeGuru.Data;
using CodeGuru.Exercises;
using Funq;
using ServiceStack.Logging;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.Razor;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints;
using System.Linq;

//The entire C# code for the stand-alone RazorRockstars demo.
namespace CourseGuru.Web
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("Unit Testing With NUnit", typeof(AppHost).Assembly) { }

        public override void Configure(Container container)
        {
            LogManager.LogFactory = new ConsoleLogFactory();
            Plugins.Add(new RazorFormat());

            SetConfig(new EndpointHostConfig
            {
                DebugMode = true,
                CustomHttpHandlers = {
                    { HttpStatusCode.NotFound, new RazorHandler("/notfound") },
                    { HttpStatusCode.Unauthorized, new RazorHandler("/login") },
                }
            });
        }
    }

    [Route("/courses")]
    public class AllCourses : IReturn<List<Course>> { } 

    public class CoursesService : Service
    {
        public object Any(AllCourses request)
        {
            //for some reason Courses cannot be sent back directly, even if typecasted back to a List
            return new List<Course> {new Courses().First()};
        }
    }
}