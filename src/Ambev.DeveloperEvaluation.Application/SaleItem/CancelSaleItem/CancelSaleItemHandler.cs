using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.SalesItem.Common;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.SalesItem.CancelSaleItem;

/// <summary>
/// Handler for processing CancelSaleItemCommand requests
/// </summary>
public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, SaleItemResult>
{
    private readonly ISalesItemRepository _salesItemRepository;
    private readonly IMapper _mapper;
    private readonly IPublisher _eventPublisher;

    /// <summary>
    /// Initializes a new instance of CancelSaleItemHandler
    /// </summary>
    /// <param name="salesItemRepository">The sales item repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CancelSaleItemHandler(
        ISalesItemRepository salesItemRepository,
        IMapper mapper,
        IPublisher eventPublisher)
    {
        _salesItemRepository = salesItemRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    /// <summary>
    /// Handles the CancelSaleItemCommand request
    /// </summary>
    /// <param name="request">The CancelSaleItem command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the sale item cancellation</returns>
    public async Task<SaleItemResult> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleItemCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var saleItem = await _salesItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (saleItem == null)
            throw new KeyNotFoundException($"SaleItem with ID {request.Id} not found.");

        if (saleItem.IsCancelled)
            throw new KeyNotFoundException($"SaleItem with ID {request.Id} already canceled.");

        saleItem.Cancel();
        await _salesItemRepository.UpdateAsync(saleItem, cancellationToken);
        await _eventPublisher.Publish(new ItemCancelledEvent(saleItem));

        return _mapper.Map<SaleItemResult>(saleItem);
    }
}
