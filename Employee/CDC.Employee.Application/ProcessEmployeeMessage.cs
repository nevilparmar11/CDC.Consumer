using CDC.Employee.Application.Service;
using CDC.Employee.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CDC.Employee.Application
{
    public class ProcessEmployeeMessage : IProcessMessage
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<ProcessEmployeeMessage> _logger;

        public ProcessEmployeeMessage(IEmployeeService employeeService, ILogger<ProcessEmployeeMessage> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        public async Task ProcessKafkaMessage(ConsumedMessage consumedMessage)
        {
            try
            {
                EmployeeMessage employeeMessage = JsonConvert.DeserializeObject<EmployeeMessage>(consumedMessage.Message);
                await _employeeService.ProcessEmployee(employeeMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while processing employee message.");
            }
        }
    }
}
