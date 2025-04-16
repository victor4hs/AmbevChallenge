using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handler for processing CancelSaleCommand requests
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, SaleResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;
    private readonly IPublisher _eventPublisher;

    /// <summary>
    /// Initializes a new instance of CancelSaleHandler
    /// </summary>
    /// <param name="salesRepository">The sales repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CancelSaleHandler(
        ISalesRepository salesRepository,
        IMapper mapper,
        IPublisher eventPublisher)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    /// <summary>
    /// Handles the CancelSaleCommand request
    /// </summary>
    /// <param name="request">The CancelSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the sale cancellation</returns>
    public async Task<SaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _salesRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found.");

        if (sale.IsCancelled)
            throw new KeyNotFoundException($"Sale with ID {request.Id} already canceled.");

        sale.Cancel();
        await _salesRepository.UpdateAsync(sale, cancellationToken);
        await _eventPublisher.Publish(new SaleCancelledEvent(sale));

        return _mapper.Map<SaleResult>(sale);
    }
}
