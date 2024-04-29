using PieShop.Models;

namespace PieShop.Repository
{
    public class MockCategoryRepository : ICategoryRepository
    {

        public IEnumerable<Category> AllCategories =>
        
             new List<Category>
                {
                    new Category { CategoryId = 1, CategoryName = "Fruit Pies", Description = "All fruity pies" },
                    new Category { CategoryId = 2, CategoryName = "Cheese cake", Description = "Cheesy all the way" },
                    new Category { CategoryId = 3, CategoryName = "Seasonl pies", Description = "Get in the mood for a seasonal pie" },
                    new Category { CategoryId = 4, CategoryName = "Seasonl pies 2.0", Description = "Get in the mood for a seasonal pie" }

                };
        
    }
}
