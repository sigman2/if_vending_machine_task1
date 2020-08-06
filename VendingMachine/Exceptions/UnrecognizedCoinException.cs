using System;

namespace VendingMachine.Exceptions
{
    public class UnrecognizedCoinException : Exception
    {
        public UnrecognizedCoinException(string message)
            : base(message)
        {
        }
    }
}