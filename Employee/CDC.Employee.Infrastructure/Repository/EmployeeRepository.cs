using CDC.Employee.Infrastructure.Repository.Interface;
using CDC.Employee.Infrastructure.Context.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CDC.Employee.Infrastructure.Context;
using System;

namespace CDC.Employee.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ISqlExtension _sqlExtension;
        private readonly IContextFactory contextFactory;
        public EmployeeRepository(ISqlExtension sqlExtension, IContextFactory contextFactory)
        {
            _sqlExtension = sqlExtension;
            this.contextFactory = contextFactory;
        }

        public async Task<int> DeleteEmployee(Model.Employee employee)
        {
            int result = 0;

            using (var dbContext = contextFactory.CreateCDCDbContext<EmployeeContext>(null))
            {
                await dbContext.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    dbContext.Entry(employee).State = EntityState.Deleted;
                    result = await dbContext.SaveChangesAsync().ConfigureAwait(false);
                }).ConfigureAwait(false);
            }

            return result;
        }

        public async Task<Model.Employee> GetEmployee(Guid GlobalEmployeeId)
        {
            string query = @"Select * from dbo.Employees WHERE GlobalEmployeeId = @GlobalEmployeeId";
            return await _sqlExtension.Get<Model.Employee>(query, new { GlobalEmployeeId = GlobalEmployeeId }).ConfigureAwait(false);
        }

        public async Task<int> SaveEmployee(Model.Employee employee)
        {
            int result = 0;

            using (var dbContext = contextFactory.CreateCDCDbContext<EmployeeContext>(null))
            {
                await dbContext.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    dbContext.Add(employee);
                    result = await dbContext.SaveChangesAsync().ConfigureAwait(false);
                }).ConfigureAwait(false);
            }

            return result;
        }

        public async Task<int> UpdateEmployee(Model.Employee employee)
        {
            int result = 0;

            using (var dbContext = contextFactory.CreateCDCDbContext<EmployeeContext>(null))
            {
                await dbContext.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    dbContext.Entry(employee).State = EntityState.Modified;
                    result = await dbContext.SaveChangesAsync().ConfigureAwait(false);
                }).ConfigureAwait(false);
            }

            return result;
        }
    }
}
