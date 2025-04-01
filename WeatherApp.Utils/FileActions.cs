using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Utility
{
    public static class FileActions
    {
        public static List<WeatherData> ProcessFile(IFormFile file)
        {
            var weatherDataList = new List<WeatherData>();

            using (var stream = file.OpenReadStream())
            {
                IWorkbook workbook = Path.GetExtension(file.FileName) == ".xls"
                    ? new HSSFWorkbook(stream)
                    : new XSSFWorkbook(stream);

                foreach (ISheet sheet in workbook)
                {
                    weatherDataList.AddRange(ProcessSheet(sheet));
                }
            }

            return weatherDataList;
        }

        public static List<WeatherData> ProcessSheet(ISheet sheet)
        {
            var weatherDataList = new List<WeatherData>();

            for (int rowIndex = 4; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                IRow row = sheet.GetRow(rowIndex);
                if (row == null) continue;

                var weatherData = ParseWeatherDataRow(row);
                if (weatherData != null)
                {
                    weatherDataList.Add(weatherData);
                }
            }

            return weatherDataList;
        }


        public static WeatherData? ParseWeatherDataRow(IRow row)
        {
            return new WeatherData
            {
                Date = ParseFunctions.ParseDate(row.GetCell(0)),
                Time = ParseFunctions.ParseTime(row.GetCell(1)),
                Temperature = ParseFunctions.ParseDouble(row.GetCell(2)),
                Humidity = ParseFunctions.ParseDouble(row.GetCell(3)),
                DewPoint = ParseFunctions.ParseDouble(row.GetCell(4)),
                Pressure = ParseFunctions.ParseInt(row.GetCell(5)),
                WindDirection = ParseFunctions.ParseString(row.GetCell(6)),
                WindSpeed = ParseFunctions.ParseInt(row.GetCell(7)),
                Cloudiness = ParseFunctions.ParseInt(row.GetCell(8)),
                CloudBaseHeight = ParseFunctions.ParseInt(row.GetCell(9)),
                Visibility = ParseFunctions.ParseInt(row.GetCell(10)),
                WeatherDescription = ParseFunctions.ParseString(row.GetCell(11)),
            };
        }

    }
}
