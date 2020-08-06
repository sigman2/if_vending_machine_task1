using NUnit.Framework;
using System;
using VendingMachine.Exceptions;

namespace VendingMachine
{
    [TestFixture()]
    public class UserTests
    {
        [Test()]
        public void GetMachineManufacturer()
        {
            VendingMachine machine = new VendingMachine();
            Assert.AreEqual("SIA Sigman & CO vending operations", machine.Manufacturer);
        }

        [Test()]
        public void InsertValidCoins()
        {
            VendingMachine machine = new VendingMachine();
            machine.InsertCoin(new Money(2, 0));
            machine.InsertCoin(new Money(0, 50));
            machine.InsertCoin(new Money(0, 5));
            Assert.AreEqual(new Money(2, 55), machine.Amount);
        }

        [Test()]
        public void InsertInvalidCoin()
        {
            VendingMachine machine = new VendingMachine();
            Assert.Throws<UnrecognizedCoinException>(() => machine.InsertCoin(new Money(0, 2)));
            Assert.AreEqual(new Money(0, 0), machine.Amount);
        }

        [Test()]
        public void InsertCentsMoreThanEuro()
        {
            VendingMachine machine = new VendingMachine();
            machine.InsertCoin(new Money(0, 50));
            machine.InsertCoin(new Money(0, 50));
            machine.InsertCoin(new Money(0, 5));
            Assert.AreEqual(new Money(1, 5), machine.Amount);
        }

        [Test()]
        public void CorrectAmountReturned()
        {
            VendingMachine machine = new VendingMachine();
            machine.InsertCoin(new Money(1, 0));
            machine.InsertCoin(new Money(0, 50));
            Assert.AreEqual(new Money(1, 50), machine.ReturnMoney());
            Assert.AreEqual(new Money(0, 0), machine.Amount);
        }

        [Test()]
        public void NothingReturned()
        {
            VendingMachine machine = new VendingMachine();
            Assert.Throws<NothingToReturnException>(() => machine.ReturnMoney());
        }

        // Asserts that correct product is purchased, that available qty is deducted and insertedAmount left is correct.
        [Test()]
        public void BuyProductSuccessfully()
        {
            VendingMachine machine = new VendingMachine();
            machine.Products = new Product[] { new Product("Chips", new Money(1, 20), 10), new Product("Water", new Money(0, 70), 6) };
            machine.InsertCoin(new Money(1, 0));
            Assert.AreEqual("Water", machine.Buy(1).Name);
            Assert.AreEqual(5, machine.Products[1].Available);
            Assert.AreEqual(new Money(0, 30), machine.Amount);
        }

        // Asserts that it is not possible to buy with insertedAmount < price, available qty and insertedAmount not deducted.
        [Test()]
        public void BuyProductWithInsufficientAmount()
        {
            VendingMachine machine = new VendingMachine();
            machine.Products = new Product[] { new Product("Chips", new Money(1, 20), 10) };
            machine.InsertCoin(new Money(1, 0));
            Assert.Throws<InsufficientFundsException>(() => machine.Buy(0));
            Assert.AreEqual(10, machine.Products[0].Available);
            Assert.AreEqual(new Money(1, 0), machine.Amount);
        }

        // Asserts that it is not possible to buy with product availability = 0 and that insertedAmount is not deducted.
        [Test()]
        public void BuyProductWithNoAvailability()
        {
            Money moneyToInsert = new Money(1, 0);
            VendingMachine machine = new VendingMachine();
            machine.Products = new Product[] { new Product("Water", new Money(0, 70), 0) };
            machine.InsertCoin(moneyToInsert);
            Assert.Throws<ProductNotAvailableException>(() => machine.Buy(0));
            Assert.AreEqual(0, machine.Products[0].Available);
            Assert.AreEqual(moneyToInsert, machine.Amount);
        }

        // Asserts that it is not possible to buy non existing product and that insertedAmount is not deducted.
        [Test()]
        public void BuyNonExistingProduct()
        {
            Money moneyToInsert = new Money(1, 0);
            VendingMachine machine = new VendingMachine();
            machine.Products = new Product[] { new Product("Water", new Money(0, 70), 0) };
            machine.InsertCoin(moneyToInsert);
            Assert.Throws<ArgumentException>(() => machine.Buy(1));
            Assert.AreEqual(moneyToInsert, machine.Amount);
        }
    }
}