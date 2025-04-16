using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateSaleItem
{
    public class CreateSaleItemCommand
    {
        /// <summary>
        /// The unique identifier of the product associated with this sale item.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// The quantity of the product to be sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
