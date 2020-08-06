namespace VendingMachine
{
    public struct Product
    {
        /// <summary>Get or set product name.</summary>
        public string Name { get; set; }

        /// <summary>Get or set product price.</summary>
        public Money Price { get; set; }

        /// <summary>Get or set available amount of product.</summary>
        public int Available { get; set; }

        public Product(string productName, Money price, int qtyAvailable)
        {
            this.Name = productName;
            this.Price = price;
            this.Available = qtyAvailable;
        }
    }
}