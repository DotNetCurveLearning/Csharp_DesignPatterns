using StrategyPattern.FirstLook.Strategies.SalesTax;

namespace StrategyPattern.FirstLook.Business.Models
{
    public class Order
    {
        public Dictionary<Item, int> LineItems { get; } = new Dictionary<Item, int>();
        public ShippingDetails? ShippingDetails { get; set; }

        public decimal TotalPrice => LineItems.Sum(item => item.Key.Price * item.Value);

        public ISalesTaxStrategy? SalesTaxStrategy { get; set; }

        public decimal GetTax(ISalesTaxStrategy? salesTaxStrategy = default)
        {
            // if any tax strategy is passed, it will be used that one
            // set on our context
            var strategy = salesTaxStrategy ?? SalesTaxStrategy;

            if (strategy == null)
            {
                return 0m;
            }

            return strategy.GetTaxFor(this);
        }
    }
}
