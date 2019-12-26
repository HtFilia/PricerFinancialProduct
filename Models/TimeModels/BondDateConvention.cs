using System;
using System.Collections.Generic;
using System.Text;

namespace PricerProduitFinancier
{
    class BondDateConvention : FinancialDate
    {

        public BondDateConvention()
        {
            DateTime today = new DateTime();
            if (today.Day == 30 || today.Day == 31)
            {
                Day = 1;
                if (today.Month == 12)
                {
                    Month = 1;
                    Year = today.Year + 1;
                }
                else
                {
                    Month = today.Month + 1;
                    Year = today.Year;
                }
            }
            else
            {
                Day = today.Day + 1;
                Month = today.Month;
                Year = today.Year;
            }
        }

        public BondDateConvention(int year, int month, int day)
        {
            if (!IsValidDate(year, month, day))
            {
                throw new ArgumentException("Invalid 30/360 date. Month should be in 1-12 range. Day should be in 1-30 range.");
            }
            Year = year;
            Month = month;
            Day = day;
        }

        public override bool IsValidDate(int year, int month, int day)
        {
            if (month <= 0 || month > 12 || day <= 0 || day > 30)
            {
                return false;
            }
            return true;
        }

        public override FinancialDate GetNextDate()
        {
            if (Day < 30)
            {
                return new BondDateConvention(Year, Month, Day + 1);
            }
            if (Month < 12)
            {
                return new BondDateConvention(Year, Month + 1, 1);
            }
            return new BondDateConvention(Year + 1, 1, 1);
        }

        public override int GetNumberDaysInMonth(int month)
        {
            return 30;
        }

        public override int GetNumberDaysInYear(int year)
        {
            return 360;
        }

        public override int GetDaysBetween(FinancialDate date)
        {
            if (date.GetType() != GetType())
            {
                throw new ArgumentException("Date must be of same type.");
            }
            return 360 * (date.Year - Year) + 30 * (date.Month - Month) + date.Day - Day;
        }

        public override FinancialDate GetNextCouponDate(FinancialDate maturity, int frequency)
        {
            int day = maturity.Day;
            int month = GetNextMonth(maturity, frequency);
            int year = GetNextYear(frequency, month);
            return new BondDateConvention(year, month, day);
        }

        private int GetNextMonth(FinancialDate maturity, int frequency)
        {
            List<int> possibleMonths = GetMonthsCoupon(maturity, frequency);
            if (Day < maturity.Day)
            {
                return FindClosestBefore(possibleMonths, Month);
            }
            return FindClosestAfter(possibleMonths, Month);
        }

        private int GetNextYear(int frequency, int nextMonth)
        {
            if (nextMonth < Month || frequency == 12)
            {
                return Year + 1;
            }
            return Year;
        }

        private List<int> GetMonthsCoupon(FinancialDate maturity, int frequency)
        {
            List<int> possibleMonths = new List<int>();
            for (int month = 1; month <= 12; month++)
            {
                if (mod(month - maturity.Month, frequency) == 0)
                {
                    possibleMonths.Add(month);
                }
            }
            return possibleMonths;
        }

        private int FindClosestBefore(List<int> months, int month)
        {
            int distance = month - months[0];
            int idx = 0;
            int goodIdx = 0;
            while (distance > 0 && idx < months.Count)
            {
                if (month - months[idx] < distance)
                {
                    distance = month - months[idx];
                    goodIdx = idx;
                }
                idx++;
            }
            return months[goodIdx];
        }

        private int FindClosestAfter(List<int> months, int month)
        {
            int distance = month - months[0];
            int idx = 0;
            int goodIdx = 0;
            while (distance > 0 && idx < months.Count)
            {
                if (month - months[idx] < distance)
                {
                    distance = month - months[idx];
                    goodIdx = idx;
                }
                idx++;
            }
            return months[goodIdx + 1 < months.Count ? goodIdx + 1 : 0];
        }

        private static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public override string ToString()
        {
            return Day + "/" + Month + "/" + Year;
        }
    }
}