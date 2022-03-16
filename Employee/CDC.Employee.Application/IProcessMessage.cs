using CDC.Employee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDC.Employee.Application
{
    public interface IProcessMessage
    {
        Task ProcessKafkaMessage(ConsumedMessage consumedMessage);
    }
}
