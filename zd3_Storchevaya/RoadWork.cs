using System;

namespace zd3_Storchevaya
{
    public class RoadWork
    {
        public double Width { get; set; } //ширина 
        public double Length { get; set; } //длина дороги
        public double MassPerSquareMeter { get; set; } //масса поккрытия на 1 кв.м
        public string Location { get; set; } //местоположение
        public DateTime StartDate { get; set; } //дата начала работ

        public RoadWork(double width, double length, double massPerSquareMeter,
                        string location, DateTime startDate)
        {
            Width = width;
            Length = length;
            MassPerSquareMeter = massPerSquareMeter;
            Location = location;
            StartDate = startDate;
        }

        public virtual double GetQuality() // функция качества Q
        {
            return (Width * Length * MassPerSquareMeter) / 1000;
        }

        public virtual string GetInfo() // вывод информации
        {
            return $"Дорога: {Location}, Ширина: {Width}м, Длина: {Length}м, " +
                   $"Масса: {MassPerSquareMeter}кг/м², Дата: {StartDate:dd.MM.yyyy}, " +
                   $"Q = {GetQuality():F2} тонн";
        }
    }
}