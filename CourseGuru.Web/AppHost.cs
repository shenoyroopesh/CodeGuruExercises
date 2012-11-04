using System.Collections.Generic;
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
    [Route("/courses/{Id}")]
    public class  WebCourse : IReturn<Course>
    {
        public int Id { get; set; }
    } 

    public class CoursesService : Service
    {
        public object Any(WebCourse request)
        {
            return new Courses().First(p => request.Id == default(int) || p.Id == request.Id);
        }
    }

    [Route("/levels/{CourseId}/{LevelNo}")]
    public class WebCourseLevel : IReturn<Level>
    {
        public int CourseId { get; set; }
        public int LevelNo { get; set; }
    }

    public class LevelsService : Service
    {
        public object Get(WebCourseLevel request)
        {
            return new Courses()
                .First(p => p.Id == request.CourseId)
                .Levels
                .First(q => q.Number == request.LevelNo);
        }
    }

}