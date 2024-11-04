using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models.TaskModels;
using TaskManagement.Application.Features.Tasks.Commands.CreateTask;

namespace TaskManagement.API.Controllers;

[Route("api/tasks")]
[ApiController]
[Authorize]
public class TasksController(ISender _sender) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        var command = request.Adapt<CreateTaskCommand>();
        var result = await _sender.Send(command);
        var response = result.Adapt<CreateTaskResponse>();
        return Created($"/tasks/{response.Id}", response);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateTaskRequest request)
    {
        var command = request.Adapt<CreateTaskCommand>();
        var result = await _sender.Send(command);
        var response = result.Adapt<CreateTaskResponse>();
        return Created($"/tasks/{response.Id}", response);
    }
}
