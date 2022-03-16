using CDC.Employee.Core;
using CDC.Employee.Core.Extensions;
using CDC.Employee.Infrastructure.Repository.Interface;
using CDC.Employee.Model;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CDC.Employee.Application.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task ProcessEmployee(EmployeeMessage employeeMessage)
        {
            if(employeeMessage.EventName.Equals(nameof(EmployeeEvents.EmployeeCreated)))
            {
                Model.Employee employee = await _employeeRepository.GetEmployee(employeeMessage.Employee.GlobalEmployeeId);
                if(employee == null)
                {
                    employee = MapDexMessageToDBModel(employeeMessage);
                    await _employeeRepository.SaveEmployee(employee);
                    _logger.LogInformation("Synced - EmployeeCreated");
                }
                else
                {
                    _logger.LogInformation("Employee Already Exists!");
                }
            } 
            else if (employeeMessage.EventName.Equals(nameof(EmployeeEvents.EmployeeUpdated)))
            {
                Model.Employee employee = await _employeeRepository.GetEmployee(employeeMessage.Employee.GlobalEmployeeId);
                if (employee != null)
                {
                    employee = MapDexMessageToDBModel(employeeMessage);
                    await _employeeRepository.UpdateEmployee(employee);
                    _logger.LogInformation("Synced - EmployeeUpdated");
                }
                else
                {
                    _logger.LogInformation("Employee Does not Exist!");
                }
            }
            else if (employeeMessage.EventName.Equals(nameof(EmployeeEvents.EmployeeDeleted)))
            {
                Model.Employee employee = await _employeeRepository.GetEmployee(employeeMessage.Employee.GlobalEmployeeId);
                if (employee != null)
                {
                    employee = MapDexMessageToDBModel(employeeMessage);
                    await _employeeRepository.DeleteEmployee(employee);
                    _logger.LogInformation("Synced - EmployeeDeleted");
                }
                else
                {
                    _logger.LogInformation("Employee Does not Exist!");
                }
            }
        }

        private Model.Employee MapDexMessageToDBModel(EmployeeMessage employeeMessage)
        {
            return new Model.Employee()
            {
                GlobalEmployeeId = employeeMessage.Employee.GlobalEmployeeId,
                EmployeeId = employeeMessage.Employee.EmployeeId,
                Email = employeeMessage.Employee.Email,
                FirstName = employeeMessage.Employee.FirstName,
                LastName = employeeMessage.Employee.LastName,
                HiredDate = employeeMessage.Employee.HiredDate.ToDateTime(),
                PhoneNumber = employeeMessage.Employee.PhoneNumber,
            };
        }
    }
}
