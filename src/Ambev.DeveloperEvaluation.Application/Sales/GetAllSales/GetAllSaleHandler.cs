using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Handler for processing <see cref="GetSaleCommand"/> requests.
/// </summary>
public class GetAllSaleHandler : IRequestHandler<GetAllSaleCommand, List<SaleResult>>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandler"/> class.
    /// </summary>
    /// <param name="salesRepository">The sales repository for accessing sale data.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    public GetAllSaleHandler(
        ISalesRepository salesRepository,
        IMapper mapper)
    {
        _salesRepository = salesRepository;
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
    public async Task<List<SaleResult>> Handle(GetAllSaleCommand request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<SaleQueryFilter>(request);

        var sale = await _salesRepository.GetAllAsync(filter, cancellationToken);
        if (sale == null || sale.Count() == 0)
            throw new KeyNotFoundException($"Sale not found for specific filter");

        return _mapper.Map<List<SaleResult>>(sale);
    }
}