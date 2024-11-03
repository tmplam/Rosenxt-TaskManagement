using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models.UserModels;
using TaskManagement.Application.Features.Users.Commands.RegisterUser;
using TaskManagement.Application.Features.Users.Queries.Login;

namespace TaskManagement.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(ISender _sender) : ControllerBase
{
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] RegisterUserRequest request)
    {
        var command = request.Adapt<RegisterUserCommand>();
        var result = await _sender.Send(command);
        var response = result.Adapt<RegisterUserResponse>();
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var query = request.Adapt<LoginQuery>();
        var result = await _sender.Send(query);
        var response = result.Adapt<LoginResponse>();
        return Ok(response);
    }
}
