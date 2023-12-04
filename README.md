# ASP.NET Core Session Extension
A simple ASP.NET Core Session Extension for storing and retrieving objects in JSON format.

## How to Use
### Session Extension Methods
This extension includes three methods for working with session data:

- SetObjectAsJson: Stores an object in the session as a JSON-formatted string.
- GetObjectFromJson: Retrieves an object from the session and deserializes it from JSON.
- SetStr and GetStr: Provides similar functionality for string values.

## Startup Configuration
Make sure to add the required session-related services in the Startup class:

```
...
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    //Default session timeout is 20 min.
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
#region Singleton
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#endregion
...
app.UseSession();
...
app.Run();
```

## Usage in HomeController
```
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
      
 }
```
