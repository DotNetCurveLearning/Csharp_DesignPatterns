using StrategyPattern.FirstLook.Business.Models;

namespace StrategyPattern.FirstLook.Strategies.Shipping
{
    public class SwedishPostalServiceShippingStrategy : IShippingStrategy
    {
        public void Ship(Order order)
        {
            using (var client = new HttpClient())
            {
                // TODO: Implement PostNord Shipping Integration
                Console.WriteLine("Order is shipped with Post Nord.");
            }
        }
    }
}
