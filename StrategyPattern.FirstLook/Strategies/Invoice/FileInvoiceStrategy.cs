using StrategyPattern.FirstLook.Business.Models;

namespace StrategyPattern.FirstLook.Business.Strategies.Invoice
{
    public class FileInvoiceStrategy : InvoiceStrategy
    {
        /// <summary>
        /// To save on file the order's invoice text representation
        /// </summary>
        /// <param name="order"></param>
        public override void Generate(Order order)
        {
            using (var stream = new StreamWriter($"invoice_{Guid.NewGuid()}.txt"))
            {
                stream.Write(GenerateTextInvoice(order));
                stream.Flush();
            }
        }
    }
}