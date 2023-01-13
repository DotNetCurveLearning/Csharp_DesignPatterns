using StrategyPattern.FirstLook.Business.Models;

namespace StrategyPattern.FirstLook.Strategies.SalesTax
{
    public class SwedenSalesTaxStrategy : ISalesTaxStrategy
    {
        public decimal GetTaxFor(Order order)
        {
            decimal totalTax = 0m;

            foreach (var item in order.LineItems)
            {
                // item.Key
            }

            var destination = order.ShippingDetails.DestinationCountry.ToLowerInvariant();
            var origin = order.ShippingDetails.OriginCountry.ToLowerInvariant();

            if (destination == origin)
            {
                return order.TotalPrice * 0.25m;
            }

            return 0;
        }
    }
}
