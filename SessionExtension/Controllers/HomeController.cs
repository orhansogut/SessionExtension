using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SessionExtension.Helper;
using SessionExtension.Models;
using System.Diagnostics;

namespace SessionExtension.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISession _session;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Index()
        {
            //Creating new User object
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Username = "SessionHelper",
                FirstName = "Session",
                LastName = "Extension",
                Email = "session@helper.com",
                BirthDate = DateTime.Now
            };
            //User object is stored in the session with the key "UserModel" in JSON format.
            _session.SetObjectAsJson("UserModel", user);
            //This allows access to the User object in any context.
            User userModelFromSession = _session.GetObjectFromJson<User>("UserModel");

            //For Example String
            _session.SetStr("Username", user.Username);
            string usernameFromSession = _session.GetStr("Username");

            string json = JsonConvert.SerializeObject(new
            {
                UserJSON = userModelFromSession,
                Username = usernameFromSession
            });
            return Ok(json);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
