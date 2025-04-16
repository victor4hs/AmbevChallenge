using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.SalesItem.Common;

namespace Ambev.DeveloperEvaluation.Application.SalesItem.UpdateSaleItem;

/// <summary>
/// Handler for processing <see cref="UpdateSaleItemCommand"/> requests.
/// </summary>
public class UpdateSaleItemHandler : IRequestHandler<UpdateSaleItemCommand, SaleItemResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly ISalesItemRepository _salesItemRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleItemHandler"/> class.
    /// </summary>
    /// <param name="salesRepository">The sales repository for managing sale data.</param>
    /// <param name="salesItemRepository">The sales item repository for managing sale data.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    public UpdateSaleItemHandler(ISalesRepository salesRepository, IMapper mapper, ISalesItemRepository salesItemRepository)
    {
        _salesRepository = salesRepository;
        _salesItemRepository = salesItemRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the <see cref="UpdateSaleItemCommand"/> request.
    /// </summary>
    /// <param name="command">The command containing the sale details to be updated.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains 
    /// the <see cref="SaleItemResult"/> with the details of the updated sale.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown when the command validation fails.
    /// </exception>
    public async Task<SaleItemResult> Handle(UpdateSaleItemCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleItemCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingSaleItem = await _salesItemRepository.GetByIdAsync(command.Id, cancellationToken) 
            ?? throw new KeyNotFoundException($"SaleItem with ID {command.Id} not found.");

        var saleItem = _mapper.Map<SaleItem>(command);

        if (saleItem.IsCancelled)
        {
            saleItem.Cancel();
        }

        existingSaleItem.UpdateFrom(saleItem);

        var existingSale = await _salesRepository.GetByIdAsync(existingSaleItem.SaleId, cancellationToken)
            ?? throw new KeyNotFoundException($"Sale with ID {saleItem.Id} not found.");

        var salesItemUpdate = existingSale.SaleItems
            .Where(x => x.Id != existingSaleItem.Id).ToList();

        salesItemUpdate.Add(existingSaleItem);
        existingSale.UpdateSaleItem(salesItemUpdate);
        existingSale.CalculateTotalAmount();

        await _salesRepository.UpdateAsync(existingSale, cancellationToken);

        return _mapper.Map<SaleItemResult>(existingSaleItem);
    }
}