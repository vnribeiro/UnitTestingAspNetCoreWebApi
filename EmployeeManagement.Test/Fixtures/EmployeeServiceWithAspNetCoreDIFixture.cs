using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Test.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Test.Fixtures
{
    public class EmployeeServiceWithAspNetCoreDIFixture : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;

        /// <summary>
        /// This is an example how to implement class fixture approach using dependency Injection.
        /// </summary>
        public EmployeeServiceWithAspNetCoreDIFixture()
        {
            var services = new ServiceCollection();
            services.AddScoped<EmployeeFactory>();
            services.AddScoped<IEmployeeManagementRepository, 
                EmployeeManagementTestDataRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            //Build provider
            _serviceProvider = services.BuildServiceProvider();
        }

        public IEmployeeManagementRepository EmployeeManagementTestDataRepository =>
            _serviceProvider.GetService<IEmployeeManagementRepository>()!;

        public IEmployeeService EmployeeService =>
            _serviceProvider.GetService<IEmployeeService>()!;

        public void Dispose()
        {
            // Clean up the setup code, if required.
        }
    }
}
