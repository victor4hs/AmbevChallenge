using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Common;

/// <summary>
/// Represents the result of an operation, containing information about success, errors, status, and returned data.
/// </summary>
public class Result<T>
{
    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// A collection of validation errors associated with the operation.
    /// </summary>
    public IEnumerable<ValidationFailure>? Errors { get; set; }

    /// <summary>
    /// The HTTP status code associated with the operation result.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// The data returned by the operation, if applicable.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// The total number of items returned, useful for pagination.
    /// </summary>
    public long TotalItems { get; set; }

    /// <summary>
    /// Default constructor for the Result class.
    /// </summary>
    public Result() { }

    /// <summary>
    /// Retrieves the details of validation errors in a simplified format.
    /// </summary>
    /// <returns>A list of <see cref="ValidationErrorDetail"/> objects representing the errors.</returns>
    public List<ValidationErrorDetail> GetErrorDetail()
    {
        if (Errors == null || !Errors.Any())
            return new();

        return Errors.Select(f => (ValidationErrorDetail)f).ToList();
    }

    /// <summary>
    /// Creates a successful result with the provided data.
    /// </summary>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="data">The data returned by the operation.</param>
    /// <param name="totalItems">The total number of items returned (optional).</param>
    /// <returns>An instance of <see cref="Result{T}"/> representing success.</returns>
    public static Result<T> Ok(int statusCode, T? data, long totalItems = 0)
        => new Result<T>()
        {
            Success = true,
            StatusCode = statusCode,
            Data = data,
            TotalItems = totalItems
        };

    /// <summary>
    /// Creates a failure result with the provided errors.
    /// </summary>
    /// <param name="errors">A collection of validation errors.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <returns>An instance of <see cref="Result{T}"/> representing failure.</returns>
    public static Result<T> Failure(IEnumerable<ValidationFailure> errors, int statusCode)
        => new Result<T>()
        {
            Success = false,
            StatusCode = statusCode,
            Errors = errors
        };
}