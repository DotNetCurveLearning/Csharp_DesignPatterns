using StrategyPattern.FirstLook.Business.Models;

namespace StrategyPattern.FirstLook.Strategies.SalesTax
{
    public class USAStateSalesTaxStrategy : ISalesTaxStrategy
    {
        public decimal GetTaxFor(Order order)
        {
            return order.ShippingDetails.DestinationState.ToLowerInvariant() switch
            {
                "la" => order.TotalPrice * 0.095m,
                "ny" => order.TotalPrice * 0.095m,
                "nyc" => order.TotalPrice * 0.095m,
                _ => 0m
            };
        }
    }
}
