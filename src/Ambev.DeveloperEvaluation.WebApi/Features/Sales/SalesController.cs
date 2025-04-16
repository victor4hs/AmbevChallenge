using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing Sales operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SalesController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new Sales
    /// </summary>
    /// <param name="request">The Sales creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Sales details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<SaleResponse>
        {
            Success = true,
            Message = "Sales created successfully",
            Data = _mapper.Map<SaleResponse>(response)
        });
    }

    /// <summary>
    /// Update a Sales
    /// </summary>
    /// <param name="request">The Sales update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The update Sales details</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSale([FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<SaleResponse>
        {
            Success = true,
            Message = "Sales update successfully",
            Data = _mapper.Map<SaleResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a Sales by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Sales</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sales details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSalesById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleRequest { Id = id };
        var validator = new GetSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetSaleCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<SaleResponse>
        {
            Success = true,
            Message = "Sales retrieved successfully",
            Data = _mapper.Map<SaleResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a Sales by their ID
    /// </summary>
    /// <param name="request">The unique identifier of the Sales</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sales details if found</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<List<SaleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllSales([FromQuery] GetAllSalesQuery saleQuery, CancellationToken cancellationToken)
    {
        var validator = new GetAllSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(saleQuery, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var getAllSaleCommand = new GetAllSaleCommand(saleQuery);

        var response = await _mediator.Send(getAllSaleCommand, cancellationToken);

        return Ok(new ApiResponseWithData<List<SaleResponse>>
        {
            Success = true,
            Message = "Sales retrieved successfully",
            Data = _mapper.Map<List<SaleResponse>>(response)
        });
    }


    /// <summary>
    /// Retrieves a Sales by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Sales</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sales details if found</returns>
    [HttpPut("{id}/cancel")]
    [ProducesResponseType(typeof(ApiResponseWithData<SaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new CancelSaleRequest { Id = id };
        var validator = new CancelSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CancelSaleCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);
        
        return Ok(new ApiResponseWithData<SaleResponse>
        {
            Success = true,
            Message = "Sales canceled successfully",
            Data = _mapper.Map<SaleResponse>(response)
        });
    }

}
