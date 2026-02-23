using E_Commerce_APIs.Application.DTOs;
using E_Commerce_APIs.Application.Features.Brands.Commands.CreateBrand;
using E_Commerce_APIs.Application.Features.Brands.Commands.DeleteBrand;
using E_Commerce_APIs.Application.Features.Brands.Commands.UpdateBrand;
using E_Commerce_APIs.Application.Features.Brands.Queries.GetBrandById;
using E_Commerce_APIs.Application.Features.Brands.Queries.GetBrands;
using E_Commerce_APIs.Application.Features.Vendors.Commands.CreateVendor;
using E_Commerce_APIs.Application.Features.Vendors.Commands.DeleteVendor;
using E_Commerce_APIs.Application.Features.Vendors.Commands.UpdateVendor;
using E_Commerce_APIs.Application.Features.Vendors.Queries.GetVendorById;
using E_Commerce_APIs.Application.Features.Vendors.Queries.GetVendors;
using E_Commerce_APIs.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_APIs.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IMediator _mediator;
    public BrandController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(Result<BrandDTO>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Result<BrandDTO>>> CreateBrand([FromBody] CreateBrandCommand request)
    {
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<BrandDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetBrands([FromQuery] GetBrandsQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }


    [HttpGet("{brandId:int}")]
    [ProducesResponseType(typeof(Result<BrandDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BrandDTO>> GetBrandById(int brandId)
    {
        var query = new GetBrandByIdQuery() { Id = brandId };
        var response = await _mediator.Send(query);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPatch("{brandId:int}")]
    [ProducesResponseType(typeof(Result<BrandDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<Result<BrandDTO>>> UpdateBrand([FromBody] UpdateBrandCommand request, int brandId)
    {
        request.Id = brandId;
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }


    [HttpDelete("{brandId:int}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<Result>> DeleteBrand(int brandId)
    {
        var command = new DeleteBrandCommand() { Id = brandId };
        var response = await _mediator.Send(command);
        if (response.IsSuccess)
            return NoContent();
        return StatusCode(response.StatusCode, response);
    }
}
