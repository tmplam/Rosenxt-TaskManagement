using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models.UserModels;
using TaskManagement.Application.Features.Users.Commands.RegisterUser;

namespace TaskManagement.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(ISender _sender) : ControllerBase
{
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(RegisterUserRequest request)
    {
        var command = request.Adapt<RegisterUserCommand>();
        var result = await _sender.Send(command);
        var response = result.Adapt<RegisterUserResponse>();
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        return Ok();
    }
}
