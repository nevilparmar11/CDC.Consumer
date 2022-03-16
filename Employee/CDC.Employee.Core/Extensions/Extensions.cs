using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDC.Employee.Core.Extensions
{
    public static class Extensions
    {
        public static DateTime? ToDateTime(this DateTimeOffset? dateTimeOffset)
        {
            if (dateTimeOffset.HasValue)
            {
                return dateTimeOffset.Value.DateTime;
            }
            return null;
        }
    }
}
