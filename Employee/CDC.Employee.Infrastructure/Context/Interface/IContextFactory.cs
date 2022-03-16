using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CDC.Employee.Infrastructure.Context.Interface
{
    public interface IContextFactory : IDesignTimeDbContextFactory<DbContext>
    {
        DbContext CreateCDCDbContext<T>(string[] args) where T : DbContext;
    }
}
