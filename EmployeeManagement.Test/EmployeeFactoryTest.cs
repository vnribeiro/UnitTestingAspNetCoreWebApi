using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
    public class EmployeeFactoryTest
    {
        /// <summary>
        /// Salary must be 2500 for internal employees.
        /// </summary>
        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
        {
            //Arrange
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee) 
                employeeFactory.CreateEmployee("John", "Doe");

            //assert
            Assert.Equal(2500, employee.Salary);
        }
    }
}