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

        /// <summary>
        /// Salary must be between 2500 and 3500 for internal employees.
        /// </summary>
        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500()
        {
            //Arrange
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)
                employeeFactory.CreateEmployee("John", "Doe");

            //assert
            Assert.True(employee.Salary is >= 2500 and <= 3500, "Salary not in acceptable range");
        }

        /// <summary>
        /// Salary must be between 2500 and 3500 for internal employees (Alternative).
        /// </summary>
        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_Alternative()
        {
            //Arrange
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)
                employeeFactory.CreateEmployee("John", "Doe");

            //assert
            Assert.True(employee.Salary >= 2500);
            Assert.True(employee.Salary <= 3500);
        }

        /// <summary>
        /// Salary must be between 2500 and 3500 for internal employees (Alternative with InRange).
        /// </summary>
        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_AlternativeWithInRange()
        {
            //Arrange
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)
                employeeFactory.CreateEmployee("John", "Doe");

            //assert
            Assert.InRange(employee.Salary, 2500, 3500);
        }

        /// <summary>
        /// Salary must be between 2500 and 3500 for internal employees (Alternative with InRange).
        /// </summary>
        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500_PrecisionExample()
        {
            //Arrange
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)
                employeeFactory.CreateEmployee("John", "Doe");
            employee.Salary = 2500.123m;

            //assert
            Assert.Equal(2500, employee.Salary, 0);
        }
    }
}