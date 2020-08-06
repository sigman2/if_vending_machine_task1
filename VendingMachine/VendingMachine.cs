using System;
using VendingMachine.Exceptions;

namespace VendingMachine
{
    public class VendingMachine : IVendingMachine
    {
        public string ManufacturedBy = "SIA Sigman & CO vending operations";
        int[] AllowedEuroCoins = { 1, 2 };
        int[] AllowedCentsCoins = { 5, 10, 20, 50 };
        public Money insertedAmount = new Money(0, 0);

        // Returns vending machine manufacturer.
        public string Manufacturer
        {
            get
            {
                return ManufacturedBy;
            }
        }

        // Returns inserted money amount.
        public Money Amount
        {
            get
            {
                return insertedAmount;
            }
        }

        public Product[] Products { get; set; }

        // As in real life only one coin at a time can be inserted so no need for additional logic.
        // Keeps adding for previously added amount, if cents exceeds 100 those are converted to euro.
        // Throws UnrecognizedCoinException.
        public Money InsertCoin(Money amount)
        {
            if (Array.IndexOf(AllowedEuroCoins, amount.Euros) >= 0 || Array.IndexOf(AllowedCentsCoins, amount.Cents) >= 0)
            {
                insertedAmount.Euros += amount.Euros;
                insertedAmount.Cents += amount.Cents;
                if (insertedAmount.Cents >= 100)
                {
                    insertedAmount.Euros += 1;
                    insertedAmount.Cents -= 100;
                }
                return amount;
            }
            else
            {
                throw new UnrecognizedCoinException(string.Format("Coin {0}.{1} not recognized!", amount.Euros, amount.Cents));
            }
        }

        // Returns current insertedAmount and resets it to 0 euros and 0 cents.
        public Money ReturnMoney()
        {

            if (Money.ConvertMoneyToDouble(insertedAmount) > 0)
            {
                Money amountToReturn = insertedAmount;
                insertedAmount = new Money(0, 0);
                return amountToReturn;
            }
            else
            {
                throw new NothingToReturnException("Inserted amount is 0.00, nothing to return!");
            }
        }

        // Ensures buying available products if insertedAmount > price, calculates remaining amount after purchase.
        // Does money conversions for comparison and to make deductions of insertedAmount.
        // Throws exceptions -  InsufficientFundsException, ProductNotAvailableException, IndexOutOfRangeException.
        public Product Buy(int productNumber)
        {
            try
            {
                double availableAmount = Money.ConvertMoneyToDouble(insertedAmount);
                double price = Money.ConvertMoneyToDouble(Products[productNumber].Price);

                if (availableAmount < price)
                {
                    throw new InsufficientFundsException(string.Format("Available amount {0} is less than product price {1}!", availableAmount, price));
                }

                if (Products[productNumber].Available > 0)
                {
                    Products[productNumber].Available = Products[productNumber].Available - 1;
                    double remainingAmount = availableAmount - price;
                    insertedAmount = Money.ConvertDoubleToMoney(remainingAmount);
                    return Products[productNumber];
                }
                else
                {
                    throw new ProductNotAvailableException(string.Format("Product with number {0} is not available!", productNumber));
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentException(string.Format("Product with number {0} is not found!", productNumber));
            }
        }
    }
}