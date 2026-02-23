using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Features.Users.Commands.LoginUser;
using E_Commerce_APIs.Application.Features.Users.Commands.RegisterUser;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_APIs.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;


    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<AuthResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<AuthResponseDto>>> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<AuthResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<AuthResponseDto>>> Login([FromBody] LoginUserCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}


