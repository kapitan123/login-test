namespace UserLoginFormTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string message = "")
    {
        if (!string.IsNullOrWhiteSpace(message))
        {
            ViewBag.Message = message;
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

}