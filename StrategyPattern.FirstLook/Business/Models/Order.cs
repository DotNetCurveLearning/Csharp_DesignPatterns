using StrategyPattern.FirstLook.Strategies.SalesTax;
namespace StrategyPattern.FirstLook.Business.Models
{
    public class Order
    {
        public Dictionary<Item, int> LineItems { get; } = new Dictionary<Item, int>();
        public IList<Payment> SelectedPayments { get; } = new List<Payment>();
        public IList<Payment> FinalizedPayments { get; } = new List<Payment>();

        public decimal AmountDue => TotalPrice - FinalizedPayments.Sum(payment => payment.Amount);
        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.WaitingForPayment;
        public ShippingDetails? ShippingDetails { get; set; }

        public decimal TotalPrice => LineItems.Sum(item => item.Key.Price * item.Value);

        public ISalesTaxStrategy? SalesTaxStrategy { get; set; }
        public IInvoiceStrategy? InvoiceStrategy { get; set; }
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

        public void FinalizeOrder()
        {
            if (SelectedPayments.Any(payment => payment.PaymentProvider == PaymentProvider.Invoice) &&
                AmountDue > 0 &&
                ShippingStatus == ShippingStatus.WaitingForPayment)
            {
                InvoiceStrategy.Generate(this);
                ShippingStatus = ShippingStatus.ReadyForShipment;
            }
            else if (AmountDue > 0)
            {
                throw new Exception("Unable to finalize order");
            }
        }
    }
}
