﻿using StrategyPattern.FirstLook.Strategies.SalesTax;
using StrategyPattern.FirstLook.Strategies.Shipping;

namespace StrategyPattern.FirstLook.Business.Models
{
    public class Order
    {
        public Dictionary<Item, int> LineItems { get; } = new Dictionary<Item, int>();
        public IList<Payment> SelectedPayments { get; } = new List<Payment>();
        public IList<Payment> FinalizedPayments { get; } = new List<Payment>();
        public decimal AmountDue => TotalPrice - FinalizedPayments.Sum(payment => payment.Amount);
        public decimal TotalPrice => LineItems.Sum(item => item.Key.Price * item.Value);
        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.WaitingForPayment;
        public ShippingDetails? ShippingDetails { get; set; }
        public ISalesTaxStrategy? SalesTaxStrategy { get; set; }
        public IInvoiceStrategy? InvoiceStrategy { get; set; }
        public IShippingStrategy? ShippingStrategy { get; set; }
    }
}
