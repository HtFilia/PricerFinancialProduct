using System;
using System.Collections.Generic;
using System.Text;

namespace PricerProduitFinancier
{
    public sealed class FinancialProductFactory
    {
        private static Dictionary<string, Type> MappingProduct = new Dictionary<string, Type>();

        private static FinancialProductFactory Instance = null;

        private FinancialProductFactory()
        {
            MappingProduct.Add("FixedYieldBond", typeof(FixedYieldBond));
        }

        public static FinancialProductFactory GetInstance()
        {
            if (Instance == null)
            {
                Instance = new FinancialProductFactory();
            }
            return Instance;
        }

        public static void AddMapping(string query, Type concreteType)
        {
            MappingProduct.Add(query, concreteType);
        }

        public FinancialProduct GetDesiredFinancialProduct(string desiredType, List<double> yields,
            FinancialDate maturity, double faceValue, List<double> marketRates, int frequency)
        {
            if (!MappingProduct.ContainsKey(desiredType))
            {
                throw new ArgumentException(desiredType + " is not a valid FinancialProduct type.");
            }
            return Activator.CreateInstance(MappingProduct[desiredType], yields, maturity, faceValue, marketRates, frequency) as FinancialProduct;
        }
    }
}