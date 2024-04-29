using Microsoft.AspNetCore.Mvc;
using PieShop.Repository;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    public class PieController : Controller
    {

        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;


        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _pieRepository = pieRepository;
        }

        public IActionResult List()
        {
           PietListViewModel pietListViewModel = new PietListViewModel(_pieRepository.AllPies,"All Pies");

            return View(pietListViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);

            if(pie == null)
            {
                return NotFound();
            }

            return View(pie);
        }
    }
}
