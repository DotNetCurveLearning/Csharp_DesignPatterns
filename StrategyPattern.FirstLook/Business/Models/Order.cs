using StrategyPattern.FirstLook.Strategies.SalesTax;

namespace StrategyPattern.FirstLook.Business.Models
{
    public class Order
    {
        public Dictionary<Item, int> LineItems { get; } = new Dictionary<Item, int>();
        public ShippingDetails? ShippingDetails { get; set; }

        public decimal TotalPrice => LineItems.Sum(item => item.Key.Price * item.Value);

        public ISalesTaxStrategy? SalesTaxStrategy { get; set; }

        public decimal GetTax()
        {
            if (SalesTaxStrategy == null)
            {
                return 0m;
            }

            return SalesTaxStrategy.GetTaxFor(this);
        }
    }
}
