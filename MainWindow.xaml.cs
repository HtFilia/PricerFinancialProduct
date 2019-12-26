using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PricerProduitFinancier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Price(object sender, RoutedEventArgs e)
        {
            // QUICK SIMULATION
            // TYPES OF SIMULATION 
            string dateConvention = "BondDateConvention";
            string productType = "FixedYieldBond";

            // CONSTANT YIELD OF 5.5% PER YEAR
            List<double> annualRates = new List<double>
            {
                0.055
            };

            // MATURITY OF BOND
            int maturityYear = 2025;
            int maturityMonth = 10;
            int maturityDay = 15;
            FinancialDate maturity = FinancialDateFactory.GetInstance().
                GetDesiredFinancialDate(dateConvention,
                maturityYear, maturityMonth, maturityDay);

            // UNCOMMENT TO SEE THAT MATURITY IS INDEED A BONDDATECONVENTION
            // MessageBox.Show("Maturity : " + maturity.ToString());

            // FACEVALUE OF BOND
            double faceValue = 100000;

            // CONSTANT MARKET RATE OF 1.2% PER YEAR
            List<double> marketRates = new List<double>
            {
                0.012
            };

            // FREQUENCY OF PAIEMENT ONCE PER YEAR
            int frequency = 1;

            FinancialProduct product = FinancialProductFactory.GetInstance().
                GetDesiredFinancialProduct(productType,
                annualRates, maturity, faceValue, marketRates, frequency);

            // SHOW PRICE
            MessageBox.Show("Price of Product : " + Math.Round(product.GetPrice(), 2));
        }
    }
}