using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherApp.DataAccess.Data;
using WeatherApp.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace WeatherAppWeb.Controllers
{
    public class DisplayController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DisplayController(ApplicationDbContext db)
        {
            _db = db;
        }

        

        public IActionResult Display()
        {
            int pageSize = 20;
            int pageNumber = 1;

            var items = _db.WeatherReports.OrderBy(w => w.Date).ToPagedList(pageNumber, pageSize);

            var yearsList = _db.WeatherReports
                .Where(w => w.Date.HasValue)
                .Select(w => w.Date.Value.Year)
                .Distinct()
                .OrderBy(year => year)
                .ToList();

            ViewBag.Years = yearsList;
            ViewBag.Months = Enum.GetNames(typeof(Month));

            return View(items);
        }


        [HttpGet]
        public IActionResult Filter(string? month, string? year, int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            var items = _db.WeatherReports.AsQueryable();

            if (!string.IsNullOrEmpty(month) && month != "Не выбрано")
            {
                int monthNumber = (int)(Month)Enum.Parse(typeof(Month), month);
                items = items.Where(w => w.Date.HasValue && w.Date.Value.Month == monthNumber);
            }

            if (!string.IsNullOrEmpty(year) && year != "Не выбрано")
            {
                items = items.Where(w => w.Date.HasValue && w.Date.Value.Year == int.Parse(year));
            }

            var pagedItems = items.OrderBy(w => w.Date).ToPagedList(pageNumber, pageSize);

            return PartialView("_WeatherDataPartial", pagedItems);
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
