using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base($"There seems to be a problem with your request. {message}.") { }
    }
}
