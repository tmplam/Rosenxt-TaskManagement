using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models.TaskModels;
using TaskManagement.Application.Features.Tasks.Commands.CreateTask;
using TaskManagement.Application.Features.Tasks.Commands.ToggleCompleteTask;
using TaskManagement.Application.Features.Tasks.Commands.UpdateTask;

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
        var command = request.Adapt<UpdateTaskCommand>();
        var result = await _sender.Send(command);
        var response = result.Adapt<UpdateTaskResponse>();
        return Ok(response);
    }

    [HttpPost("{id:guid}/toggle-complete")]
    public async Task<IActionResult> Update([FromRoute] Guid id)
    {
        var request = new ToggleCompleteTaskRequest(id);
        var command = request.Adapt<ToggleCompleteTaskCommand>();
        var result = await _sender.Send(command);
        var response = result.Adapt<ToggleCompleteTaskResponse>();
        return Ok(response);
    }
}
