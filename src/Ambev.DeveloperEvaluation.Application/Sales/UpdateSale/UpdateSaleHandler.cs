using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing <see cref="UpdateSaleCommand"/> requests.
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, SaleResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;
    private readonly IPublisher _eventPublisher;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleHandler"/> class.
    /// </summary>
    /// <param name="salesRepository">The sales repository for managing sale data.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    public UpdateSaleHandler(ISalesRepository salesRepository, IMapper mapper, IPublisher eventPublisher)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    /// <summary>
    /// Handles the <see cref="UpdateSaleCommand"/> request.
    /// </summary>
    /// <param name="command">The command containing the sale details to be updated.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains 
    /// the <see cref="SaleResult"/> with the details of the updated sale.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown when the command validation fails.
    /// </exception>
    public async Task<SaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingSale = await _salesRepository.GetByIdAsync(command.Id, cancellationToken) 
            ?? throw new KeyNotFoundException($"Sale with ID {command.Id} not found.");

        var sale = _mapper.Map<Sale>(command);
        sale.CalculateTotalAmount();

        if (sale.IsCancelled)
        {
            sale.Cancel();
        }

        existingSale.UpdateFrom(sale);

        await _salesRepository.UpdateAsync(existingSale, cancellationToken);
        await _eventPublisher.Publish(new SaleModifiedEvent(sale));

        return _mapper.Map<SaleResult>(sale);
    }
}