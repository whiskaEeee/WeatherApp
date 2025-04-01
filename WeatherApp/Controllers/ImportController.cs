using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using EFCore.BulkExtensions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WeatherApp.DataAccess.Data;
using WeatherApp.Models;
using System.Text;
using WeatherApp.Utility;

namespace WeatherAppWeb.Controllers
{
    public class ImportController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ImportController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadExcel(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                TempData["Message"] = "�������� �����!";
                return RedirectToAction("Import");
            }

            var weatherDataList = new List<WeatherData>();
            var messages = new List<string>();

            foreach (var file in files)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    messages.Add($"���� {file.FileName} ����� �������� ������!");
                    continue;
                }

                try
                {
                    var processedData = FileActions.ProcessFile(file);
                    if (processedData.Any())
                    {
                        weatherDataList.AddRange(processedData);
                        messages.Add($"���� {file.FileName} ������� ��������� ({processedData.Count} �������).");
                    }
                    else
                    {
                        messages.Add($"���� {file.FileName} �� �������� ������.");
                    }
                }
                catch (Exception ex)
                {
                    messages.Add($"������ ��������� ����� {file.FileName}: {ex.Message}");
                }
            }

            if (weatherDataList.Any())
            {
                _db.BulkInsert(weatherDataList);
            }

            TempData["Message"] = string.Join("|", messages);

            return RedirectToAction("Import");
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
