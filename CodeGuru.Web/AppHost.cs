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

        public List<string> UserCode { get; set; }
    } 
    
    /// <summary>
    /// Return Course
    /// </summary>
    public class WebCourse
    {
        public Course Course { get; set; }
        public int CurrentLevelNo { get; set; }
        public int CurrentChallengeNo { get; set; }

        /// <summary>
        /// Gives next challenge no if it exists, else returns 0
        /// </summary>
        public int NextChallengeNo
        {
            get
            {
                return CurrentLevel == null ? 0 : CurrentLevel.Challenges.Any(p => p.ChallengeNo == CurrentChallengeNo + 1) ? CurrentChallengeNo + 1 : 0;
            }
        }

        /// <summary>
        /// User code validations if any
        /// </summary>
        public string Message { get; set; }

        public Level CurrentLevel
        {
            get { return Course.Levels.FirstOrDefault(p => p.Number == CurrentLevelNo); }
        }

        public IChallenge CurrentChallenge
        {
            get { return CurrentLevel == null ? null : CurrentLevel.Challenges.FirstOrDefault(p => p.ChallengeNo == CurrentChallengeNo); }
        }
    }

    /// <summary>
    /// Service
    /// </summary>
    public class WebCourseService : Service
    {
        public object Any(WebCourseRequest request)
        {
            return GetModelFromRequest(request);
        }

        private static WebCourse GetModelFromRequest(WebCourseRequest request)
        {
            var course = new WebCourse
            {
                Course = new Courses().First(p => p.Id == request.CourseId || request.CourseId == default(int)),
                CurrentLevelNo = request.LevelNo,
                CurrentChallengeNo = request.ChallengeNo,
            };

            if (request.UserCode != null && request.UserCode.Any()) course.Message = course.CurrentChallenge.Validate(request.UserCode);

            return course;
        }
    }
}