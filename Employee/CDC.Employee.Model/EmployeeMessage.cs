using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDC.Employee.Model
{
    public class EmployeeMessage : Dex.DataExchangeEvent
    {
        public Dex.Employee Employee { get; set; }
    }
}
