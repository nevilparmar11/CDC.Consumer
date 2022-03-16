using CDC.Employee.Model;
using System.Threading.Tasks;

namespace CDC.Employee.Application.Service
{
    public interface IEmployeeService
    {
        public Task ProcessEmployee(EmployeeMessage employeeMessage);
    }
}
