using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.UpdateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.GetSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.CancelSale;
using Ambev.DeveloperEvaluation.Application.SalesItem.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Application.SalesItem.GetSaleItem;
using Ambev.DeveloperEvaluation.Application.SalesItem.CancelSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing Sales operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesItemController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SalesController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SalesItemController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Update a Sales
    /// </summary>
    /// <param name="request">The Sales update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The update Sales details</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleItemResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSaleItem([FromBody] UpdateSaleItemRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateSaleItemCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<SaleItemResponse>
        {
            Success = true,
            Message = "Sales update successfully",
            Data = _mapper.Map<SaleItemResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a Sales by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Sales</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sales details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleItemResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleItemById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleItemRequest { Id = id };
        var validator = new GetSaleItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetSaleItemCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<SaleItemResponse>
        {
            Success = true,
            Message = "Sales item retrieved successfully",
            Data = _mapper.Map<SaleItemResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a Sales by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Sales</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sales details if found</returns>
    [HttpPut("{id}/cancel")]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleItemResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSaleItem([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new CancelSaleItemRequest { Id = id };
        var validator = new CancelSaleItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CancelSaleItemCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);
        
        return Ok(new ApiResponseWithData<SaleItemResponse>
        {
            Success = true,
            Message = "Sales canceled successfully",
            Data = _mapper.Map<SaleItemResponse>(response)
        });
    }

}
