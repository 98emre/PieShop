using Microsoft.EntityFrameworkCore;
using PieShop.Data;
using PieShop.Models;

namespace PieShop.Repository
{
    public class PieRepoistory : IPieRepository
    {
        private readonly PieShopDbContext _pieShopDbContext;

        public PieRepoistory(PieShopDbContext pieShopDbContext)
        {
            _pieShopDbContext = pieShopDbContext;
        }

        public IEnumerable<Pie> AllPies => _pieShopDbContext.Pies.Include(c => c.Category);

        public IEnumerable<Pie> PiesOfTheWeek => _pieShopDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);

        public Pie? GetPieById(int pieId) => _pieShopDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
    }
}
