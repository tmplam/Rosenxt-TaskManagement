using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models.TaskModels;
using TaskManagement.Application.Features.Tasks.Commands.CreateTask;
using TaskManagement.Application.Features.Tasks.Commands.DeleteTask;
using TaskManagement.Application.Features.Tasks.Commands.TagUsersToTask;
using TaskManagement.Application.Features.Tasks.Commands.ToggleCompleteTask;
using TaskManagement.Application.Features.Tasks.Commands.UpdateTask;
using TaskManagement.Application.Features.Tasks.Queries.GetTaskById;
using TaskManagement.Application.Features.Tasks.Queries.GetTasks;

namespace TaskManagement.API.Controllers;

[Route("api/tasks")]
[ApiController]
[Authorize]
public class TasksController(ISender _sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetTaskByIdQuery(id);
        var result = await _sender.Send(query);
        var response = result.Adapt<GetTaskByIdResponse>();
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool? isCompleted)
    {
        var query = new GetTasksQuery(isCompleted);
        var result = await _sender.Send(query);
        var response = result.Adapt<GetTasksResponse>();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        var command = request.Adapt<CreateTaskCommand>();
        var result = await _sender.Send(command);
        var response = result.Adapt<CreateTaskResponse>();
        return Created($"/tasks/{response.Id}", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTaskRequest request)
    {
        var command = request.Adapt<UpdateTaskCommand>();
        var result = await _sender.Send(command);
        var response = result.Adapt<UpdateTaskResponse>();
        return Ok(response);
    }

    [HttpPatch("{id:guid}/toggle-complete")]
    public async Task<IActionResult> ToggleComplete([FromRoute] Guid id)
    {
        var request = new ToggleCompleteTaskRequest(id);
        var command = request.Adapt<ToggleCompleteTaskCommand>();
        var result = await _sender.Send(command);
        var response = result.Adapt<ToggleCompleteTaskResponse>();
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new DeleteTaskCommand(id);
        var result = await _sender.Send(command);
        var response = result.Adapt<DeleteTaskResponse>();
        return Ok(response);
    }

    [HttpPost("tag-users")]
    public async Task<IActionResult> TagUsers([FromBody] TagUsersToTaskRequest request)
    {
        var command = request.Adapt<TagUsersToTaskCommand>();
        var result = await _sender.Send(command);
        var response = result.Adapt<TagUsersToTaskResponse>();
        return Ok(response);
    }
}
