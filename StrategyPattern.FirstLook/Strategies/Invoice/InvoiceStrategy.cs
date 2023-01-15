using StrategyPattern.FirstLook.Business.Models;
using StrategyPattern.FirstLook.Strategies.SalesTax;

namespace StrategyPattern.FirstLook.Business.Strategies.Invoice
{
    /*
    This abstract class allows to create the basic implementation of
    the IInvoiceStrategy interface.
    Whoever is inheriting from this abstract class, needs to implement the Generate() method.
    */
    public abstract class InvoiceStrategy : IInvoiceStrategy
    {
        public abstract void Generate(Order order);

        /// <summary>
        /// Generate a text invoice.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>A text invoice</returns>
        public string GenerateTextInvoice(Order order)
        {
            var invoice = $"INVOICE DATE: {DateTimeOffset.Now}{Environment.NewLine}";

            invoice += $"ID|NAME|PRICE|QUANTITY{Environment.NewLine}";

            foreach (var item in order.LineItems)
            {
                invoice += $"{item.Key.Id}|{item.Key.Name}|{item.Key.Price}|{item.Value}|{Environment.NewLine}";
            }

            invoice += Environment.NewLine + Environment.NewLine;

            var tax = order.SalesTaxStrategy == null ? 0m : order.SalesTaxStrategy.GetTaxFor(order);
            var total = order.TotalPrice + tax;

            invoice += $"TAX TOTAL: {tax}{Environment.NewLine}";
            invoice += $"TOTAL: {total}{Environment.NewLine}";

            return invoice;
        }
    }
}