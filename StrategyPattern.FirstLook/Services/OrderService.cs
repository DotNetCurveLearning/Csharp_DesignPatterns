using StrategyPattern.FirstLook.Business.Models;

namespace StrategyPattern.FirstLook.Business.Services
{
    public class OrderService : IOrderService
    {
        public Order Order { get; set; }

        public OrderService(Order order)
        {
            this.Order = order;
        }
        public void FinalizeOrder()
        {
            if (Order.SelectedPayments.Any(payment => payment.PaymentProvider == PaymentProvider.Invoice) &&
                Order.AmountDue > 0 &&
                Order.ShippingStatus == ShippingStatus.WaitingForPayment)
            {
                Order.InvoiceStrategy.Generate(Order);
                Order.ShippingStatus = ShippingStatus.ReadyForShipment;
            }
            else if (Order.AmountDue > 0)
            {
                throw new Exception("Unable to finalize order");
            }
        }

        public decimal GetTax()
        {
            // if any tax strategy is passed, it will be used that one
            // set on our context
            var strategy = Order.SalesTaxStrategy;

            if (strategy == null)
            {
                return 0m;
            }

            return strategy.GetTaxFor(Order);
        }

        public void Ship()
        {
            Order?.ShippingStrategy.Ship(Order);
        }
    }
}