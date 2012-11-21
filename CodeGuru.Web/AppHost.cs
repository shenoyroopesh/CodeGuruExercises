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

namespace CodeGuru.Web
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
    [Route("/courses/{CourseId}")]
    [Route("/levels/{CourseId}/{LevelNo}")]
    [Route("/challenges/{CourseId}/{LevelNo}/{ChallengeNo}")]
    public class  WebCourseRequest : IReturn<WebCourse>
    {
        public int CourseId { get; set; }
        public int LevelNo { get; set; }
        public int ChallengeNo { get; set; }
    } 
    
    /// <summary>
    /// Return Course
    /// </summary>
    public class WebCourse
    {
        public Course Course { get; set; }
        public int CurrentLevelNo { get; set; }
        public int CurrentChallengeNo { get; set; }

        public Level CurrentLevel
        {
            get { return Course.Levels.FirstOrDefault(p => p.Number == CurrentLevelNo); }
        }

        public IChallenge CurrentChallenge
        {
            get { return CurrentLevel == null ? null : CurrentLevel.Challenges.FirstOrDefault(p => p.LevelNo == CurrentChallengeNo); }
        }
    }

    /// <summary>
    /// Service
    /// </summary>
    public class WebCourseService : Service
    {
        public object Get(WebCourseRequest request)
        {
            return new WebCourse
                {
                    Course = new Courses().First(p => p.Id == request.CourseId || request.CourseId == default(int)), 
                    CurrentLevelNo = request.LevelNo, 
                    CurrentChallengeNo = request.ChallengeNo
                };
        }
    }
}