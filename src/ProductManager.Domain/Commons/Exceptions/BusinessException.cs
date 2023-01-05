using System;
using System.Collections.Generic;

namespace ProductManager.Domain.Commons.Exceptions
{
    public class BusinessException : CommonException
    {
        public List<string> Validations { get; } = new List<string>{};

        public BusinessException(string message) : base(message) { }
    }
}

