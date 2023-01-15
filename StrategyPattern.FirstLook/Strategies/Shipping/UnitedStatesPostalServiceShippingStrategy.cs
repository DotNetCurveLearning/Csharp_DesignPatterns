using StrategyPattern.FirstLook.Business.Models;

namespace StrategyPattern.FirstLook.Strategies.Shipping
{
    public class UnitedStatesPostalServiceShippingStrategy : IShippingStrategy
    {
        public void Ship(Order order)
        {
            using (var client = new HttpClient())
            {
                // TODO: Implement USPS Shipping Integration
                Console.WriteLine("Order is shipped with USPS.");
            }
        }
    }
}
