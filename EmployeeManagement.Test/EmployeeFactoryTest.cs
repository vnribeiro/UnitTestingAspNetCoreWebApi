using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
    public class EmployeeFactoryTest : IDisposable
    {
        private readonly EmployeeFactory _employeeFactory;

        /// <summary>
        /// Context using the constructor and dispose approach.
        /// </summary>
        public EmployeeFactoryTest()
        {
            _employeeFactory = new EmployeeFactory();
        }

        /// <summary>
        /// You need only use the dispose method if you have to clean up resources.
        /// </summary>
        public void Dispose()
        {
            // Clean up the setup code, if required.
        }

        /// <summary>
        /// Salary must be 2500 for internal employees.
        /// This test is skipped for demo reasons.
        /// </summary>
        [Fact(Skip = "Skipping this one for demo reasons")]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
        {
            //Arrange

            //Act
            var employee = (InternalEmployee)
                _employeeFactory.CreateEmployee("John", "Doe");

            //assert
            Assert.Equal(2500, employee.Salary);
        }

        /// <summary>
        /// Salary must be between 2500 and 3500 for internal employees.
        /// </summary>
        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500()
        {
            //Arrange

            //Act
            var employee = (InternalEmployee)
                _employeeFactory.CreateEmployee("John", "Doe");

            //assert
            Assert.True(employee.Salary is >= 2500 and <= 3500, "Salary not in acceptable range");
        }

        /// <summary>
        /// Salary must be between 2500 and 3500 for internal employees (Alternative).
        /// </summary>
        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_Alternative()
        {
            //Arrange

            //Act
            var employee = (InternalEmployee)
                _employeeFactory.CreateEmployee("John", "Doe");

            //assert
            Assert.True(employee.Salary >= 2500);
            Assert.True(employee.Salary <= 3500);
        }

        /// <summary>
        /// Salary must be between 2500 and 3500 for internal employees (Alternative with InRange).
        /// </summary>
        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_AlternativeWithInRange()
        {
            //Arrange

            //Act
            var employee = (InternalEmployee)
                _employeeFactory.CreateEmployee("John", "Doe");

            //assert
            Assert.InRange(employee.Salary, 2500, 3500);
        }

        /// <summary>
        /// Salary must be between 2500 and 3500 for internal employees (Alternative with InRange).
        /// </summary>
        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500_PrecisionExample()
        {
            //Arrange

            //Act
            var employee = (InternalEmployee)
                _employeeFactory.CreateEmployee("John", "Doe");
            employee.Salary = 2500.123m;

            //assert
            Assert.Equal(2500, employee.Salary, 0);
        }

        /// <summary>
        /// If external employee is true, the return must be ExternalEmployee.
        /// </summary>
        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_ReturnType")]
        public void CreateEmployee_IsExternalIsTrue_ReturnMustBeExternalEmployee()
        {
            //Arrange

            //Act
            var employee = _employeeFactory.CreateEmployee("John", "Doe", "Marvin", true);

            //assert
            Assert.IsType<ExternalEmployee>(employee);

            //Asset variation
            //Assert.IsAssignableFrom<Employee>(employee);
        }
    }
}