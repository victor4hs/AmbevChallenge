using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing <see cref="CreateSaleCommand"/> requests.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;
    private readonly IPublisher _eventPublisher;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
    /// </summary>
    /// <param name="salesRepository">The sales repository for managing sale data.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    public CreateSaleHandler(
        ISalesRepository salesRepository, 
        IMapper mapper,
        IPublisher eventPublisher)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    /// <summary>
    /// Handles the <see cref="CreateSaleCommand"/> request.
    /// </summary>
    /// <param name="command">The command containing the sale details to be created.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains 
    /// the <see cref="CreateSaleResult"/> with the details of the created sale.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown when the command validation fails.
    /// </exception>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Sale>(command);
        sale.SetSaleRules();
        sale.CalculateTotalAmount();
        var createdSale = await _salesRepository.CreateAsync(sale, cancellationToken);
        await _eventPublisher.Publish(new SaleCreatedEvent(sale));

        return _mapper.Map<CreateSaleResult>(createdSale);
    }
}