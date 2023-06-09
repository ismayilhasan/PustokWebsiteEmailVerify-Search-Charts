using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P328Pustok.DAL;
using System.Collections.Generic;

namespace P328Pustok.Areas.Manage.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Member")]
    [Area("manage")]
    public class DashboardController : Controller
    {
        private readonly PustokContext _context;

        public DashboardController(PustokContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult getPieChartDatas()
        {
            var genreNames = _context.Genres.Select(x => x.Name).ToList();
            var genreBookCount = _context.Genres.Include(x => x.Books).Select(x => x.Books.Count).ToList();
            List<string> colors = new List<string>();

            for (int i = 0; i < genreNames.Count; i++)
            {
                Random random = new Random();
                string randomColor = string.Format("#{0:X6}", random.Next(0x1000000));
                colors.Add(randomColor);
            }

            return Json(new
            {
                GenreNames = genreNames,
                GenreBookCount = genreBookCount,
                Colors = colors
            });

        }

        public IActionResult getLinearChartDatas()
        {


            return Json(new
            {

                Labels = new string[] { "Yanvdsar", "Fevral", "Mart" }
            });

        }
    }
}
