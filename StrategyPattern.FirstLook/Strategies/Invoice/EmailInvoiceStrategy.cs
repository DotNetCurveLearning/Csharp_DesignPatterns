using System.Net;
using System.Net.Mail;
using StrategyPattern.FirstLook.Business.Models;

namespace StrategyPattern.FirstLook.Business.Strategies.Invoice
{
    public class EmailInvoiceStrategy : InvoiceStrategy
    {
        /// <summary>
        /// To send an email with the order's invoice text representation
        /// </summary>
        /// <param name="order"></param>
        public override void Generate(Order order)
        {
            var body = GenerateTextInvoice(order);

            using (SmtpClient client = new SmtpClient("smtp.mail.yahoo.com", 587))
            {
                NetworkCredential credentials = new NetworkCredential("ernestoacostacuba@yahoo.com.mx", "cbcoa1975");
                client.Credentials = credentials;

                MailMessage mail = new MailMessage("YOUR EMAIL", "YOUR EMAIL")
                {
                    Subject = "We've created an invoice for your order",
                    Body = GenerateTextInvoice(order)
                };

                client.Send(mail);
            }
        }
    }
}