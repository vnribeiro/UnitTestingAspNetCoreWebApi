using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Test.Services;

namespace EmployeeManagement.Test.Fixtures
{
    public class EmployeeServiceFixture : IDisposable
    {
        public EmployeeService EmployeeService { get; }
        public IEmployeeManagementRepository EmployeeManagementTestDataRepository { get; }

        /// <summary>
        /// This is an example how to implement class fixture approach.
        /// </summary>
        public EmployeeServiceFixture()
        {
            EmployeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            EmployeeService = new EmployeeService(EmployeeManagementTestDataRepository, new EmployeeFactory());
        }

        public void Dispose()
        {
            // Clean up the setup code, if required.
        }
    }
}
