using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Exceptions
{
    public class BaseException : Exception
    {
        protected BaseException(string message) 
            : base(message)
        {
        }
    }
}
