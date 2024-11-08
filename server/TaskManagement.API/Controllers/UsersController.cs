using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models.UserModels;
using TaskManagement.Application.Features.Users.Queries.SearchEmail;

namespace TaskManagement.API.Controllers;

[Route("api/users")]
[ApiController]
[Authorize]
public class UsersController(ISender _sender) : ControllerBase
{
    [HttpGet("email-search")]
    public async Task<IActionResult> SearchEmail([FromQuery] string keyword)
    {
        var query = new SearchEmailQuery(keyword);
        var result = await _sender.Send(query);
        var response = result.Adapt<SearchEmailResponse>();
        return Ok(response);
    }
}
