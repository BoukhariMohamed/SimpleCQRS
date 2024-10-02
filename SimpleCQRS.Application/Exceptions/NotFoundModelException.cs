using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCQRS.Application.Exceptions
{
    public class NotFoundModelException : CustomException
    {
        public NotFoundModelException(string entityName, object key)
            : base($"{entityName} with key '{key}' was not found.")
        {

        }
    }
}
