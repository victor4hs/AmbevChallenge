using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.Common;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing <see cref="UpdateSaleCommand"/> requests.
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, SaleResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleHandler"/> class.
    /// </summary>
    /// <param name="salesRepository">The sales repository for managing sale data.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    public UpdateSaleHandler(ISalesRepository salesRepository, IMapper mapper)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
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
        // Validate the command using the UpdateSaleCommandValidator
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Map the command to a Sale entity
        var sale = _mapper.Map<Sale>(command);

        // Update the sale in the repository
        var updatedSale = await _salesRepository.UpdateAsync(sale, cancellationToken);

        // Map the updated sale entity to the result object
        return _mapper.Map<SaleResult>(updatedSale);
    }
}