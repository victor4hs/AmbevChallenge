using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.SalesItem.Common;

namespace Ambev.DeveloperEvaluation.Application.SalesItem.GetSaleItem;

/// <summary>
/// Handler for processing <see cref="GetSaleItemCommand"/> requests.
/// </summary>
public class GetSaleItemHandler : IRequestHandler<GetSaleItemCommand, SaleItemResult>
{
    private readonly ISalesItemRepository _salesItemRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandler"/> class.
    /// </summary>
    /// <param name="salesRepository">The sales repository for accessing sale item data.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    public GetSaleItemHandler(
        ISalesItemRepository salesItemRepository,
        IMapper mapper)
    {
        _salesItemRepository = salesItemRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the <see cref="GetSaleCommand"/> request.
    /// </summary>
    /// <param name="request">The command containing the ID of the sale to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains 
    /// the <see cref="SaleResult"/> with the details of the retrieved sale.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown when the command validation fails.
    /// </exception>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when the sale with the specified ID is not found.
    /// </exception>
    public async Task<SaleItemResult> Handle(GetSaleItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleItemCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var saleItem = await _salesItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (saleItem == null)
            throw new KeyNotFoundException($"SaleItem with ID {request.Id} not found");

        return _mapper.Map<SaleItemResult>(saleItem);
    }
}