using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Buisiness.Exceptions
{
    public class EmployeeException : Exception // Custom exception related to employees
    {
        public EmployeeException(string message) : base(message) 
        {
            
        }
    }
}
