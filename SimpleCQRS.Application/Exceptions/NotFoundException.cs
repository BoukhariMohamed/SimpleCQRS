using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCQRS.Application.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string entityName, object key)
            : base($"{entityName} with key '{key}' was not found.")
        {

        }
    }
}
