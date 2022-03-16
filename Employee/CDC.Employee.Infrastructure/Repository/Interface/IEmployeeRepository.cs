using System;
using System.Threading.Tasks;

namespace CDC.Employee.Infrastructure.Repository.Interface
{
    public interface IEmployeeRepository
    {
        public Task<Model.Employee> GetEmployee(Guid globalEmployeeId);
        public Task<int> SaveEmployee(Model.Employee employee);
        public Task<int> UpdateEmployee(Model.Employee employee);
        public Task<int> DeleteEmployee(Model.Employee employee);
    }
}
