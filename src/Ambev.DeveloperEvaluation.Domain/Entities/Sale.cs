using Ambev.DeveloperEvaluation.Domain.Common;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction, including details about the customer, branch, and items sold.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Unique identifier for the sale (e.g., invoice number).
        /// </summary>
        public string SaleNumber { get; private set; }

        /// <summary>
        /// The date and time when the sale was made.
        /// </summary>
        public DateTime SaleDate { get; private set; }

        /// <summary>
        /// The customer who made the purchase.
        /// </summary>
        public string CustomerName { get; private set; }

        /// <summary>
        /// Identifier of the customer who made the purchase.
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// The branch where the sale was made.
        /// </summary>
        public string BranchName { get; private set; }

        /// <summary>
        /// Identifier of the branch where the sale was made.
        /// </summary>
        public Guid BranchId { get; private set; }

        /// <summary>
        /// The total amount of the sale, including all items and discounts.
        /// </summary>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// The collection of items included in the sale.
        /// </summary>
        public ICollection<SaleItem> SaleItems { get; private set; }

        /// <summary>
        /// Default constructor for Sale.
        /// </summary>
        public Sale() { }

        /// <summary>
        /// Constructor to initialize a Sale with all required properties.
        /// </summary>
        /// <param name="saleNumber">Unique identifier for the sale.</param>
        /// <param name="saleDate">The date and time of the sale.</param>
        /// <param name="customerId">The customer id who made the purchase.</param>
        /// <param name="customerName">The customer who made the purchase.</param>
        /// <param name="branchId">The branch id where the sale was made.</param>
        /// <param name="branchName">The branch where the sale was made.</param>
        /// <param name="totalAmount">The total amount of the sale.</param>
        /// <param name="isCancelled">Indicates if the sale is cancelled.</param>
        /// <param name="items">The collection of items in the sale.</param>
        public Sale(string saleNumber, DateTime saleDate, Guid customerId, string customerName,
                    Guid branchId, string branchName, decimal totalAmount,
                    bool isCancelled, ICollection<SaleItem> items)
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchName = branchName;
            TotalAmount = totalAmount;
            IsCancelled = isCancelled;
            SaleItems = items;
        }
    }
}