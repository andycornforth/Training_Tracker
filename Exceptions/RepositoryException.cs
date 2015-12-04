using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message) : base($"There seems to be a problem with your request. {message}.") { }
    }
}
