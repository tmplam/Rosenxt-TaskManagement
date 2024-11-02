using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("sign-up")]
    public IActionResult SignUp()
    {
        return null;
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        return null;
    }
}
