using System.Text.Json.Serialization;
using StrategyPattern.FirstLook.Business.Models;
using Newtonsoft.Json;

namespace StrategyPattern.FirstLook.Business.Strategies.Invoice
{
    public class PrintOnDemandInvoiceStrategy : InvoiceStrategy
    {
        public override void Generate(Order order)
        {
            using (var client = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(order);

                client.BaseAddress = new Uri("https://pluralsight.com");
                client.PostAsync("/print-on-demand", new StringContent(content));
            }
        }
    }
}