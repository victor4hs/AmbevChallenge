using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item in a sale, containing details about the product, quantity, pricing, and discounts.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Identifier of the sale to which this item belongs.
        /// </summary>
        public Guid SaleId { get; private set; }

        /// <summary>
        /// The product name associated with this sale item.
        /// </summary>
        public string? ProductName { get; private set; }

        /// <summary>
        /// Identifier of the product being sold.
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Quantity of the product being sold.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Unit price of the product at the time of sale.
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Discount applied to this sale item.
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// Total price for this sale item after applying discounts.
        /// </summary>
        public decimal TotalPrice { get; private set; }

        /// <summary>
        /// Indicates whether this sale item has been cancelled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Default constructor for SaleItem.
        /// </summary>
        public SaleItem() { }

        /// <summary>
        /// Constructor to initialize a SaleItem with all required properties.
        /// </summary>
        /// <param name="saleId">Identifier of the sale.</param>
        /// <param name="productId">Identifier of the product.</param>
        /// <param name="productName">The product name being sold.</param>
        /// <param name="unitPrice">Unit price of the product.</param>
        /// <param name="quantity">Quantity of the product being sold.</param>
        /// <param name="discount">Discount applied to the sale item.</param>
        /// <param name="totalPrice">Total price after discount.</param>
        /// <param name="isCancelled">Indicates if the item is cancelled.</param>
        public SaleItem(Guid saleId, Guid productId, string productName, decimal unitPrice,
            int quantity, decimal discount, decimal totalPrice, bool isCancelled)
        {
            SaleId = saleId;
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Discount = discount;
            TotalPrice = totalPrice;
            IsCancelled = isCancelled;
        }
        public void UpdateFrom(SaleItem updatedItem)
        {
            ProductName = updatedItem.ProductName;
            ProductId = updatedItem.ProductId;
            Quantity = updatedItem.Quantity;
            UnitPrice = updatedItem.UnitPrice;
            IsCancelled = updatedItem.IsCancelled;
        }

        public ValidationResult Validate()
        {
            var validator = new SaleItemValidator();
            return validator.Validate(this);
        }

        public void Cancel()
        {
            IsCancelled = true;
        }

        public void SetSaleItem(Guid saleId)
        {
            SaleId = saleId;
            GetDiscountPercentage();
            CalculatePrices();
        }

        private void CalculatePrices()
        {
            decimal subtotal = UnitPrice * Quantity;
            decimal discountValue = subtotal * Discount;

            TotalPrice = subtotal - discountValue;
        }

        private void GetDiscountPercentage()
        {
            if (Quantity >= 10 && Quantity <= 20)
                Discount = 0.20m;
            else if (Quantity >= 4)
                Discount = 0.10m;
            else
                Discount = 0;
        }
    }
}