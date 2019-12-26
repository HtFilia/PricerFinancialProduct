using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PricerProduitFinancier
{
    class BlackScholesModel : ShareModel
    {
        public BlackScholesModel(double trend, double vol, double marketRate,
            double initialSpot)
        {
            Trend = trend;
            Vol = vol;
            MarketRate = marketRate;
            InitialSpot = initialSpot;
        }

        public override double GetTrend()
        {
            return Trend;
        }

        public override double GetVol()
        {
            return Vol;
        }
        public override double GetMarketRate()
        {
            return MarketRate;
        }

        public override List<double> GenerateTrajectory(FinancialDate today, FinancialDate maturity,
            int nbTimeSteps, Random random)
        {
            List<double> spots = new List<double>();
            double dt = today.GetDaysBetween(maturity) / nbTimeSteps;
            for (int idx = 1; idx < nbTimeSteps; idx++)
            {
                double previousSpot = spots[idx - 1];
                double trendDiff = Math.Exp((Trend - Math.Pow(Vol, 2) / 2) * dt);
                double volDiff = Math.Exp(Vol * Math.Sqrt(dt) * Normal.Sample(random, 0.0, 1.0));
                double newSpot = previousSpot * trendDiff * volDiff;
                spots.Add(newSpot);
            }
            return spots;
        }
    }
}