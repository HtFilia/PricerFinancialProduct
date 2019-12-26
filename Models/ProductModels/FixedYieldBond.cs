using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PricerProduitFinancier
{
    class FixedYieldBond : FinancialProduct
    {
        public FixedYieldBond(List<double> annualRates, FinancialDate maturity,
            double faceValue, List<double> marketRates, int frequency)
        {
            Yields = annualRates;
            Maturity = maturity;
            FaceValue = faceValue;
            MarketRates = marketRates;
            Frequency = frequency;
            CouponDates = GetCouponDates();
        }

        protected internal override List<FinancialDate> GetCouponDates()
        {
            string dateType = Maturity.GetType().ToString();
            FinancialDate today = FinancialDateFactory.GetInstance().GetDesiredFinancialDate(dateType);
            List<FinancialDate> CouponDates = new List<FinancialDate>();
            while (!today.Equals(Maturity))
            {
                FinancialDate nextCouponDate = today.GetNextCouponDate(Maturity, Frequency);
                CouponDates.Add(nextCouponDate);
                today = nextCouponDate;
            }
            return CouponDates;
        }

        public override double GetPrice()
        {
            string dateType = Maturity.GetType().ToString();
            double price = 0;
            FinancialDate today = FinancialDateFactory.GetInstance().GetDesiredFinancialDate(dateType);
            FinancialDate previous = today;
            FinancialDate current = CouponDates[0];
            int nbDays = previous.GetDaysBetween(current);
            double factor = nbDays / previous.GetNumberDaysInYear(previous.Year);
            price += factor * FaceValue * Yields[0] / Frequency / Math.Pow(1 + MarketRates[0], factor);
            for (int idx = 1; idx < CouponDates.Count; idx++)
            {
                previous = CouponDates[idx - 1];
                current = CouponDates[idx];
                nbDays = previous.GetDaysBetween(current);
                price += FaceValue * Yields[0] / Frequency / Math.Pow(1 + MarketRates[0],
                    factor + nbDays / current.GetNumberDaysInYear(current.Year));
            }
            price += FaceValue * (1 + Yields[0]) / Math.Pow(1 + MarketRates[0], factor + today.YearsTill(Maturity));
            return price;
        }
    }
}