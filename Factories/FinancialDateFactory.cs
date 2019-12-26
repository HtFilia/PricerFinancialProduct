using System;
using System.Collections.Generic;
using System.Text;

namespace PricerProduitFinancier
{
    public sealed class FinancialDateFactory
    {
        private static Dictionary<string, Type> MappingDate = new Dictionary<string, Type>();

        private static FinancialDateFactory Instance = null;

        private FinancialDateFactory()
        {
            MappingDate.Add("BondDateConvention", typeof(BondDateConvention));
        }

        public static FinancialDateFactory GetInstance()
        {
            if (Instance == null)
            {
                Instance = new FinancialDateFactory();
            }
            return Instance;
        }

        public static void AddMapping(string query, Type concreteType)
        {
            MappingDate.Add(query, concreteType);
        }

        public FinancialDate GetDesiredFinancialDate(string desiredType)
        {
            if (!MappingDate.ContainsKey(desiredType))
            {
                throw new ArgumentException(desiredType + " is not a valid FinancialDate type.");
            }
            return Activator.CreateInstance(MappingDate[desiredType]) as FinancialDate;
        }

        public FinancialDate GetDesiredFinancialDate(string desiredType, int year, int month, int day)
        {
            if (!MappingDate.ContainsKey(desiredType))
            {
                throw new ArgumentException(desiredType + " is not a valid FinancialDate type.");
            }
            return Activator.CreateInstance(MappingDate[desiredType], year, month, day) as FinancialDate;
        }
    }
}