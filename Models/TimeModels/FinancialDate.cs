using System;
using System.Collections.Generic;
using System.Text;

namespace PricerProduitFinancier
{
    public abstract class FinancialDate
    {

        protected internal int Year;

        protected internal int Month;

        protected internal int Day;

        public abstract bool IsValidDate(int year, int month, int day);

        public abstract FinancialDate GetNextDate();

        public abstract int GetNumberDaysInMonth(int month);

        public abstract int GetNumberDaysInYear(int year);

        public abstract int GetDaysBetween(FinancialDate date);

        public int YearsTill(FinancialDate maturity) { return maturity.Year - Year; }

        public abstract FinancialDate GetNextCouponDate(FinancialDate maturity, int frequency);

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            FinancialDate date = obj as FinancialDate;
            return date.Day == Day && date.Month == Month && date.Year == Year;
        }

        public override int GetHashCode()
        {
            return Year.GetHashCode() + Month.GetHashCode() + Day.GetHashCode();
        }
    }
}