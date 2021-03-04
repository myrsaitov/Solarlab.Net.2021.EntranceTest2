using System;
using System.Collections.Generic;

namespace WidePictBoard.Application.User.Contracts.Exceptions
{
    public class IdentityException : Exception
    {
        public readonly IEnumerable<(string, string)> errors;

        public IdentityException(IEnumerable<(string, string)> errors)
        {
            this.errors = errors;
        }
    }
}