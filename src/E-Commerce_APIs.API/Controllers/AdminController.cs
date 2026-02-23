using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Features.Users.Commands.CreateUser;
using E_Commerce_APIs.Application.Features.Users.Commands.DeleteUser;
using E_Commerce_APIs.Application.Features.Users.Commands.RegisterUser;
using E_Commerce_APIs.Application.Features.Users.Commands.UpdateUser;
using E_Commerce_APIs.Application.Features.Users.Queries.GetUserById;
using E_Commerce_APIs.Application.Features.Users.Queries.GetUsers;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_APIs.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;
    public AdminController(IMediator mediator) => _mediator = mediator;

    [HttpGet("users")]
    [ProducesResponseType(typeof(PaginatedResult<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("users")]
    [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<UserDto>>> AddUser([FromBody] CreateUserCommand request)
    {
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPatch("users/{userId:guid}")]
    [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UpdateUserCommand request, Guid userId)
    {
        request.Id = userId;
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("users/{userId:guid}")]
    [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<UserDto>>> GetUserById(Guid userId)
    {
        var query = new GetUserByIdQuery() { Id = userId };
        var response = await _mediator.Send(query);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("users/{userId:guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> DeleteUser(Guid userId)
    {
        var command = new DeleteUserCommand() { Id = userId };
        var response = await _mediator.Send(command);
        if (response.IsSuccess)
            return NoContent();
        return StatusCode(response.StatusCode, response);
    }
}
