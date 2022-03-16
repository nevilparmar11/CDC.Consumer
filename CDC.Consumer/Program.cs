using CDC.Employee.Application;
using CDC.Employee.Application.Service;
using CDC.Employee.Infrastructure;
using CDC.Employee.Infrastructure.Context;
using CDC.Employee.Infrastructure.Context.Interface;
using CDC.Employee.Infrastructure.Extension;
using CDC.Employee.Infrastructure.Repository;
using CDC.Employee.Infrastructure.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CDC.Consumer
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var connString = hostContext.Configuration.GetConnectionString("CloudDBMirror");
                    services.AddHostedService<ConsumerWorker>();
                    services.AddTransient<ISqlExtension, SqlExtension>();
                    services.AddTransient<IEmployeeRepository, EmployeeRepository>();
                    services.AddTransient<IEmployeeService, EmployeeService>();
                    services.AddTransient<IProcessMessage, ProcessEmployeeMessage>();
                    services.AddTransient<IContextFactory, ContextFactory>();
                });
    }
}
