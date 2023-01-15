using StrategyPattern.FirstLook.Business.Models;

namespace StrategyPattern.FirstLook.Business.Services
{
    public interface IOrderService {
        decimal GetTax();
        void FinalizeOrder();
        void Ship();
    }
}