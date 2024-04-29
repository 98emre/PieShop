using PieShop.Models;

namespace PieShop.Repository
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies { get; }

        IEnumerable<Pie> PiesOfTheWeek { get; }

        Pie? GetPieById(int pieId);
    }
}
