using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Features.Vendors.Commands.CreateVendor;
using E_Commerce_APIs.Application.Features.Vendors.Commands.DeleteVendor;
using E_Commerce_APIs.Application.Features.Vendors.Commands.UpdateVendor;
using E_Commerce_APIs.Application.Features.Vendors.Queries.GetVendorById;
using E_Commerce_APIs.Application.Features.Vendors.Queries.GetVendors;
using E_Commerce_APIs.Application.Services;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_APIs.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorController : ControllerBase
{
    private readonly IMediator _mediator;
    public VendorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(Result<VendorDTO>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<VendorDTO>>> CreateVendor([FromBody] CreateVendorCommand request)
    {
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<VendorDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetVendors([FromQuery] GetVendorsQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }


    [HttpPost("{vendorId:guid}")]
    [ProducesResponseType(typeof(Result<VendorDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<VendorDTO>>> UpdateVendor([FromBody] UpdateVendorCommand request, Guid vendorId)
    {
        request.Id = vendorId;
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{vendorId:guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> DeleteVendor(Guid vendorId)
    {
        var command = new DeleteVendorCommand() { Id = vendorId };
        var response = await _mediator.Send(command);
        if (response.IsSuccess)
            return NoContent();
        return StatusCode(response.StatusCode, response);
    }
    [HttpGet("{vendorId:guid}")]
    [ProducesResponseType(typeof(Result<VendorDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VendorDTO>> GetVendorById(Guid vendorId)
    {
        var query = new GetVendorByIdQuery() { Id = vendorId };
        var response = await _mediator.Send(query);
        return StatusCode(response.StatusCode, response);
    }

}
