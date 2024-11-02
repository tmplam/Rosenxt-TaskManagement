using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("sign-up")]
    public IActionResult SignUp()
    {
        return Ok();
    }

    [HttpPost("login")]
    [Authorize]
    public IActionResult Login()
    {
        return Ok();
    }
}
