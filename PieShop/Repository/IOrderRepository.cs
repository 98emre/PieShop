using PieShop.Models;

namespace PieShop.Repository
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
