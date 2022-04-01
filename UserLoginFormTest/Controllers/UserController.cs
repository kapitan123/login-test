

namespace UserLoginFormTest.Controllers;

public class UserController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserRepository _userRepo;

    public UserController(ILogger<HomeController> logger, IUserRepository userRepo)
    {
        _logger = logger;
        _userRepo = userRepo;
    }

    [HttpGet]
    public IActionResult Create() => View(new CreateUserViewModel());

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserViewModel vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var doesUserExist = await _userRepo.CheckUserByEmail(vm.Email);

            if (doesUserExist)
            {
                ModelState.AddModelError(nameof(vm.Email), $"User with email {vm.Email} is already present");
                return View(vm);
            }

            await _userRepo.Create(vm.Email, vm.Password);

            return RedirectToAction("Index", "Home", new { message = "User was created." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            ModelState.AddModelError("Server error", "Internal error has happened. User can't be created. Contact support.");
            return View(vm);
        }
    }
}