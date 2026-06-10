using System;

namespace zd3_Storchevaya
{
    public class ReinforcedRoadWork : RoadWork
    {
        public int P { get; set; } //коэффициент прочности
        public string WeatherCondition { get; set; } //погодные условия
        public string Contractor { get; set; } //подрядчик

        public ReinforcedRoadWork(double width, double length, double massPerSquareMeter,
                                  string location, DateTime startDate, int p,
                                  string weatherCondition, string contractor)
            : base(width, length, massPerSquareMeter, location, startDate)
        {
            P = p;
            WeatherCondition = weatherCondition;
            Contractor = contractor;
        }

        //перекрытие функции качества Qp
        public override double GetQuality()
        {
            double Q = base.GetQuality();

            if (P >= 5 && P <= 8)
                return Q * 1.1;
            else if (P == 3 || P == 4 || P == 9 || P == 10)
                return Q * 1.6;
            else
                return Q * 1.9;
        }

        //перекрытие вывода информации
        public override string GetInfo()
        {
            return base.GetInfo() + $", P={P}, Погода={WeatherCondition}, " +
                   $"Подрядчик={Contractor}, Qp={GetQuality():F2} тонн";
        }
    }
}