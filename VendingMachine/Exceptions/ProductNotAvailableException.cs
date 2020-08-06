using System;

namespace VendingMachine.Exceptions
{
    public class ProductNotAvailableException: Exception
    {
        public ProductNotAvailableException(string message)
            :base(message)
        {
        }
    }
}