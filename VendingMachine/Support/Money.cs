using System.Globalization;

namespace VendingMachine
{
    public struct Money
    {
        /// <summary>Get or set euros.</summary>
        public int Euros { get; set; }

        /// <summary>Get or set cents.</summary>
        public int Cents { get; set; }

        public Money(int euros, int cents)
        {
            this.Euros = euros;
            this.Cents = cents;
        }

        public static double ConvertMoneyToDouble(Money money)
        {
            return double.Parse(
                       money.Euros.ToString() + "." + money.Cents.ToString(),
                       CultureInfo.InvariantCulture);
        }

        public static Money ConvertDoubleToMoney(double money)
        {
            string[] parts = money.ToString("0.00", CultureInfo.InvariantCulture).Split('.');
            return new Money(int.Parse(parts[0]), int.Parse(parts[1]));
        }
    }
}