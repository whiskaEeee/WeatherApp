using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp.Models
{
    public class WeatherData
    {
        [Key]
        public int Id { get; set; }

        [Column("Дата")]
        public DateOnly? Date { get; set; }

        [Column("Время (московское)")]
        public TimeOnly? Time { get; set; }
        
        [Column("T")]
        public double? Temperature { get; set; }

        [Column("Отн. Влажность воздуха, %")]
        public double? Humidity { get; set; }

        [Column("Td")]
        public double? DewPoint { get; set; }

        [Column("Атм. давление, мм рт.ст.")]
        public int? Pressure { get; set; }

        [Column("Направление ветра")]
        [MaxLength(20)]
        public string? WindDirection { get; set; }

        [Column("Скорость ветра, м/с")]
        public int? WindSpeed { get; set; }

        [Column("Облачность, %")]
        public int? Cloudiness { get; set; }

        [Column("h")]
        public int? CloudBaseHeight { get; set; }

        [Column("VV")]
        public int? Visibility { get; set; }

        [Column("Погодные явления")]
        [MaxLength(255)]
        public string? WeatherDescription { get; set; }
    }
}
