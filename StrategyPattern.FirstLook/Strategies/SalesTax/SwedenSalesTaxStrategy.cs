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
                totalTax = item.Key.ItemType switch {
                    ItemType.Food => totalTax + (item.Key.Price * 0.06m) * item.Value,
                    ItemType.Literature => totalTax + (item.Key.Price * 0.08m) * item.Value,
                    ItemType.Service => totalTax + (item.Key.Price * 0.25m) * item.Value,
                    ItemType.Hardware => totalTax + (item.Key.Price * 0.25m) * item.Value,
                    _ => throw new ArgumentOutOfRangeException(nameof(item.Key.ItemType), $"Not expected type value: {item.Key.ItemType}")
                };
            }

            return totalTax;
        }
    }
}
