using PieShop.Models;

namespace PieShop.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories {  get; }

    }
}
