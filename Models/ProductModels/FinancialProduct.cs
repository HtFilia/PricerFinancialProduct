using System;
using System.Collections.Generic;
using System.Text;

namespace PricerProduitFinancier
{
    public abstract class FinancialProduct
    {
        protected internal List<double> Yields;

        protected internal FinancialDate Maturity;

        protected internal double FaceValue;

        protected internal List<double> MarketRates;

        protected internal int Frequency;

        protected internal List<FinancialDate> CouponDates;

        protected internal abstract List<FinancialDate> GetCouponDates();

        public abstract double GetPrice();
    }
}