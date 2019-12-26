using System;
using System.Collections.Generic;
using System.Text;

namespace PricerProduitFinancier
{
    abstract class ShareModel
    {
        protected internal double Trend;

        protected internal double Vol;

        protected internal double MarketRate;

        protected internal double InitialSpot;

        public abstract double GetTrend();

        public abstract double GetVol();

        public abstract double GetMarketRate();

        public abstract List<double> GenerateTrajectory(FinancialDate today, FinancialDate maturity, int nbTimeSteps, Random rng);
    }
}