using StrategyPattern.FirstLook.Business.Models;
using StrategyPattern.FirstLook.Business.Services;
using StrategyPattern.FirstLook.Business.Strategies.Invoice;
using StrategyPattern.FirstLook.Strategies.SalesTax;
using StrategyPattern.FirstLook.Strategies.Shipping;

public class Program
{
    static void Main(string[] args)
    {
        IOrderService OrderService;

        #region Input
        Console.WriteLine("Please select an origin country: ");
        var origin = Console.ReadLine().Trim();

        Console.WriteLine("Please select a destination country: ");
        var destination = Console.ReadLine().Trim();

        Console.WriteLine("Choose one of the following shipping providers: ");
        Console.WriteLine("1. PostNord (Swedish Postal Service)");
        Console.WriteLine("2. DHL");
        Console.WriteLine("3. USPS");
        Console.WriteLine("4.Fedex");
        Console.WriteLine("5. UPS");

        Console.WriteLine("Select shipping provider: ");
        var provider = Convert.ToInt32(Console.ReadLine().Trim());

        Console.WriteLine("Choose one of the following invoice delivery options: ");
        Console.WriteLine("1. E-mail");
        Console.WriteLine("2. File (download later)");
        Console.WriteLine("3. Mail");

        Console.WriteLine("Select invoice delivery options: ");
        var invoiceOption = Convert.ToInt32(Console.ReadLine().Trim());
        #endregion

        var order = new Order
        {
            ShippingDetails = new ShippingDetails
            {
                OriginCountry = "Sweden",
                DestinationCountry = "Sweden"
            },
            SalesTaxStrategy = GetSalesTaxStrategyFor(origin),
            InvoiceStrategy = GetInvoiceStrategyFor(invoiceOption),
            ShippingStrategy = GetShippingStrategyFor(provider)
        };

        order.SelectedPayments.Add(new Payment { PaymentProvider = PaymentProvider.Invoice});

        order.LineItems.Add(new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m, ItemType.Literature), 1);
        order.LineItems.Add(new Item("CONSULTING", "Building a website", 100m, ItemType.Service), 1);

        order.SelectedPayments.Add(new Payment()
        {
            PaymentProvider = PaymentProvider.Invoice
        });

        try
        {
            OrderService = new OrderService(order);
            Console.WriteLine(OrderService.GetTax());
            order.InvoiceStrategy = new FileInvoiceStrategy();
            OrderService.FinalizeOrder();
            OrderService.Ship();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    /// <summary>
    /// Returns the concrete tax strategy implementation
    /// </summary>
    /// <param name="origin"></param>
    /// <returns></returns>
    private static ISalesTaxStrategy GetSalesTaxStrategyFor(string origin)
    {
        return origin.ToLowerInvariant() switch {
            "sweden" => new SwedenSalesTaxStrategy(),
            "usa" => new USAStateSalesTaxStrategy(),
            _ => throw new Exception("Unsupported region")
        };
    }

    /// <summary>
    /// Returns the concrete shipping strategy implementation
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    private static IShippingStrategy GetShippingStrategyFor(int provider)
    {
        return provider switch {
            1 => new SwedishPostalServiceShippingStrategy(),
            2 => new DhlShippingStrategy(),
            3 => new UnitedStatesPostalServiceShippingStrategy(),
            4 => new FedexShippingStrategy(),
            5 => new UpsShippingStrategy(),
            _ => throw new Exception("Unsupported shipping option")
        };
    }

    /// <summary>
    /// Returns the concrete invoice delivery strategy implementation
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    private static IInvoiceStrategy GetInvoiceStrategyFor(int provider)
    {
        return provider switch {
            1 => new EmailInvoiceStrategy(),
            2 => new FileInvoiceStrategy(),
            3 => new PrintOnDemandInvoiceStrategy(),
            _ => throw new Exception("Unsupported invoice delivery option")
        };
    }
}