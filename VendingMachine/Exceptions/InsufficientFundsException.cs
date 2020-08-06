﻿using System;

namespace VendingMachine.Exceptions
{
    public class InsufficientFundsException: Exception
    {
        public InsufficientFundsException(string message)
			:base(message)
        {
        }
    }
}