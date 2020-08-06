using System;

namespace VendingMachine.Exceptions
{
    public class NothingToReturnException : Exception
    {
        public NothingToReturnException(string message)
            :base(message)
        {
        }
    }
}