using System;
using System.Collections.Generic;
using System.Text;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    public class SubscriptionPeriod
    {
        private int _year = 2010;
        private PeriodType _type = PeriodType.Semester;

        public PeriodType PeriodType
        {
            get { return _type ; }

        }

        public int Year
        {
            get { return _year; }
           
        }

        private int _period = 1;

        public int PeriodNumber
        {
            get { return _period; }
        }


        public SubscriptionPeriod(int period, int year, PeriodType type)
        {


            if (type == PeriodType.Semester)
            {


                if ((period != 1) && (period != 2))
                    throw new ArgumentException("Ci sono solo i seguenti semestri: Semestre 1 e Semestre 2");
                _year = year;
                _period = period;
                _type = type;

                CalculateDates(year, period);
            }
            else if (type == PeriodType.Month)
            {
                if ((period < 1) || (period > 12))
                    throw new ArgumentException("Specificare un mese compreso tra 1 e 12");
                _year = year;
                _period = period;
                _type = type;
                CalculateMonthlyDate(_period, _year);

            }
            else
            {
                _year = year;
                _period = period;
                _type = type;
                _initialDate = new DateTime(year, 1, 1);
                _endDate = new DateTime(year, 12, 31);
            }


        }

        private void CalculateMonthlyDate(int _period, int _year)
        {
            _initialDate = new DateTime(_year, _period, 1);
            _endDate = new DateTime(_year, _period, DateTime.DaysInMonth(_year, _period));
        }


        public SubscriptionPeriod(DateTime initialDate, DateTime endDate)
        {
            _initialDate = initialDate;
            _endDate = endDate;

            _year = endDate.Year;
            _period = -1;
            _type = PeriodType.Interval;

        }

        public SubscriptionPeriod(int year)
        {
            _type = PeriodType.Interval;

            _year = year;
            _period = -1;

            _initialDate = new DateTime(_year, 1, 1);
            _endDate = new DateTime(_year, 12, 31);


        }

        private void CalculateDates(int year, int semester)
        {
            int iniday = 1;
            int endDay = 1;
            int iniYear = year;
            int endYear = year;
            int iniMonth = 0;
            int endMonth = 0;

            if (semester == 1)
                iniMonth = 10;
            else
                iniMonth = 4;


            if (semester == 1)
                endMonth = 3;
            else
                endMonth = 9;

            if (semester == 1)
                endDay = 31;
            else
                endDay = 30;

            if (semester == 1)
                iniYear  = year -1;
            else
                iniYear = year;






            _initialDate = new DateTime(iniYear, iniMonth, iniday);
            _endDate = new DateTime(endYear, endMonth, endDay);
        }


        private DateTime _initialDate = new DateTime(1900, 1, 1);
        
        public DateTime InitialDate
        {
            get { return _initialDate; }
        }


        private DateTime _endDate = new DateTime(1900, 1, 1);
       
        public DateTime EndDate
        {
            get { return _endDate; }
        }




        public string Validate()
        {

            StringBuilder b = new StringBuilder();


            if (_type == PeriodType.Semester)
            {

                //verifica anno

                if ((_year < 1980) || (_year > 2040))
                        b.AppendLine("Anno non corretto. Immettere dati dal 1980 fino al massimo 2040");


                //verifica semestre

                if ((_period != 1) && (_period != 2))
                        b.AppendLine("Semestre non corretto. Immettere un semestre valido");

            }
            else if (_type == PeriodType.Interval)
            {
                //verifica date

                if ((_initialDate == DateTime.MinValue) || (_initialDate == DateTime.MaxValue))
                        b.AppendLine("Data inizio non valida. ");

                //verifica data fine

                    if ((_endDate == DateTime.MinValue) || (_endDate == DateTime.MaxValue))
                        b.AppendLine("Data fine non valida. ");

                //verifica date

                    if ((_endDate < _initialDate))
                        b.AppendLine("Data fine precedente data inizio.");

            }
            else
            {
                //verifica anno

                if ((_year < 1980) || (_year > 2040))
                        b.AppendLine("Anno non corretto. Immettere dati dal 1980 fino al massimo 2040");


                //verifica semestre

                if ((_period < 1) || (_period > 12))
                        b.AppendLine("Mese non corretto. Immettere un semestre valido");
            }



            return b.ToString();
        }



    }
}
