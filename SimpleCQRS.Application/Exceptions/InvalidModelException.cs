using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCQRS.Application.Exceptions
{
    public class InvalidModelException : CustomException
    {
        public InvalidModelException(string message) : base(message)
        {
        }
    }
}
