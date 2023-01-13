using StrategyPattern.FirstLook.Business.Models;

namespace StrategyPattern.FirstLook.Strategies.SalesTax
{
    // This interface defines a contract for each concrete strategy implementation to use.
    // The strategy pattern is a behavioural design pattern and the idea is that you choose
    // a suitable strategy based on user input
    public interface ISalesTaxStrategy
    {
        decimal GetTaxFor(Order order);
    }
}
