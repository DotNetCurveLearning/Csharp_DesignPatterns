using StrategyPattern.FirstLook.Business.Models;
using StrategyPattern.FirstLook.Business.Strategies.Invoice;
using StrategyPattern.FirstLook.Strategies.SalesTax;

public class Program
{
    static void Main(string[] args)
    {
        var order = new Order
        {
            ShippingDetails = new ShippingDetails
            {
                OriginCountry = "Sweden",
                DestinationCountry = "Sweden"
            },
            SalesTaxStrategy = new SwedenSalesTaxStrategy()
        };

        order.LineItems.Add(new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m, ItemType.Literature), 1);
        order.LineItems.Add(new Item("CONSULTING", "Building a website", 100m, ItemType.Service), 1);

        order.SelectedPayments.Add(new Payment()
        {
            PaymentProvider = PaymentProvider.Invoice
        });

        Console.WriteLine(order.GetTax());

        order.InvoiceStrategy = new FileInvoiceStrategy();
        order.FinalizeOrder();
    }
}