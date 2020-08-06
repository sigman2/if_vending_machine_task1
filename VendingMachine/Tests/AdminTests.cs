using NUnit.Framework;

namespace VendingMachine
{
    [TestFixture()]
    public class AdminTests
    {
        // Add one product
        [Test()]
        public void AddProduct()
        {
            VendingMachine machine = new VendingMachine();
            Money price = new Money(1, 20);
            machine.Products = new Product[] { new Product("Chips", price, 10) };
            Assert.AreEqual(1, machine.Products.Length);
            Assert.AreEqual("Chips", machine.Products[0].Name);
            Assert.AreEqual(price, machine.Products[0].Price);
            Assert.AreEqual(10, machine.Products[0].Available);
        }

        // Add multiple products.
        [Test()]
        public void AddMultipleProducts()
        {
            VendingMachine machine = new VendingMachine();
            machine.Products = new Product[] { new Product("Chips", new Money(1, 20), 10), new Product("Water", new Money(0, 70), 6), new Product("Nuts", new Money(1, 0), 2) };
            Assert.AreEqual(3, machine.Products.Length);
            Assert.AreEqual("Nuts", machine.Products[2].Name);
            Assert.AreEqual(new Money(0, 70), machine.Products[1].Price);
            Assert.AreEqual(10, machine.Products[0].Available);
        }

        // Change product available quantity.
        [Test()]
        public void RestockProduct()
        {
            int restockQty = 5;
            VendingMachine machine = new VendingMachine();
            machine.Products = new Product[] { new Product("Water", new Money(0, 70), 0) };
            machine.Products[0].Available = restockQty;
            Assert.AreEqual(restockQty, machine.Products[0].Available);
        }

        // Change product name.
        [Test()]
        public void ChangeProductName()
        {
            string newName = "Nuts";
            VendingMachine machine = new VendingMachine();
            machine.Products = new Product[] { new Product("Water", new Money(0, 70), 0) };
            machine.Products[0].Name = newName;
            Assert.AreEqual(newName, machine.Products[0].Name);
        }

        // Change product price.
        [Test()]
        public void ChangeProductPrice()
        {
            Money newPrice = new Money(1, 0);
            VendingMachine machine = new VendingMachine();
            machine.Products = new Product[] { new Product("Water", new Money(0, 70), 0) };
            machine.Products[0].Price = newPrice;
            Assert.AreEqual(newPrice, machine.Products[0].Price);
        }

        // Reset products.
        [Test()]
        public void ResetProducts()
        {
            VendingMachine machine = new VendingMachine();
            string name = "Chips";
            Money chipsPrice = new Money(1, 20);
            int chipsQty = 2;
            machine.Products = new Product[] { new Product("Water", new Money(0, 70), 6), new Product("Nuts", new Money(1, 0), 2) };
            machine.Products = new Product[] { new Product(name, chipsPrice, chipsQty) };
            Assert.AreEqual(1, machine.Products.Length);
            Assert.AreEqual(name, machine.Products[0].Name);
            Assert.AreEqual(chipsPrice, machine.Products[0].Price);
            Assert.AreEqual(chipsQty, machine.Products[0].Available);
        }
    }
}