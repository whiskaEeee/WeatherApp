using Microsoft.VisualBasic;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Utility
{
    public static class ParseFunctions
    {
        public static DateOnly? ParseDate(ICell cell)
        {
            if (cell == null) return null;
            return DateOnly.TryParse(cell.ToString(), out var result) ? result : null;
        }

        public static TimeOnly? ParseTime(ICell cell)
        {
            if (cell == null) return null;
            return TimeOnly.TryParse(cell.ToString(), out var result) ? result : null;
        }

        public static string? ParseString(ICell cell)
        {
            if (cell == null || cell.CellType == CellType.Blank)
                return null;

            string value = cell.ToString().Trim();
            return string.IsNullOrEmpty(value) ? null : value;
        }

        public static double? ParseDouble(ICell cell)
        {
            if (cell == null) return null;

            try
            {
                if (cell.CellType == CellType.Numeric)
                {
                    return cell.NumericCellValue;
                }
                else if (cell.CellType == CellType.String)
                {
                    return double.TryParse(cell.StringCellValue.Replace(',', '.'),
                        NumberStyles.Float, CultureInfo.InvariantCulture, out var result)
                        ? result
                        : null;
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        public static int? ParseInt(ICell cell)
        {
            if (cell == null) return null;
            return int.TryParse(cell.ToString(), out var result) ? result : null;
        }
    }

}
