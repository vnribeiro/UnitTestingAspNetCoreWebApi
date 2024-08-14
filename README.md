# EmployeeManagement

### The repository is an example of how you can implement unit tests using XUnit

## Overview

The `EmployeeManagement` solution is designed to manage employee data, including handling business logic, data access, and unit testing. The solution is divided into several projects, each with a specific responsibility.

## Projects

### 1. EmployeeManagement.Business

This project contains the core business logic for managing employees. It includes services, event arguments, and custom exceptions.

- **Services**: Classes that handle business operations like giving raises, calculating bonuses, etc.
- **EventArguments**: Custom event arguments used in business events.
- **Exceptions**: Custom exceptions for handling business-specific errors.

### 2. EmployeeManagement.DataAccess

This project is responsible for data access and entity management. It includes entity classes that represent the data model.

- **Entities**: Classes that represent the data structure of employees and other related entities.

### 3. EmployeeManagement.Test

This project contains unit tests for the `EmployeeManagement` solution. It uses xUnit as the testing framework and includes fixtures and services for setting up test environments.

- **Fixtures**: Classes that provide setup and teardown logic for tests.
- **Services**: Mock or stub services used in testing.
- **Tests**: Unit tests for validating the business logic.

## Unit Testing

The unit tests are written using the xUnit framework. The tests are organized into collections and classes to ensure proper setup and teardown of shared resources.

### Example Test
```csharp

using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;
using EmployeeManagement.Test.TestData;

namespace EmployeeManagement.Test
{
    [Collection("EmployeeServiceCollection")]
    public class DataDriveEmployeeServiceTests
    {
        private readonly EmployeeServiceFixture _employeeServiceFixture;

        public DataDriveEmployeeServiceTests(EmployeeServiceFixture employeeServiceFixture)
        {
            _employeeServiceFixture = employeeServiceFixture;
        }

        [Fact]
        public async Task GiveRaise_MinimumRaiseGiven_EmployeeMinimumRaiseGivenMustBeTrue()
        {
            // Arrange  
            var internalEmployee = new InternalEmployee(
                "Brooklyn", "Cannon", 5, 3000, false, 1);

            // Act
            await _employeeServiceFixture
                .EmployeeService.GiveRaiseAsync(internalEmployee, 100);

            // Assert
            Assert.True(internalEmployee.MinimumRaiseGiven);
        }


        [Fact]
        public async Task GiveRaise_MoreThanMinimumRaiseGiven_EmployeeMinimumRaiseGivenMustBeFalse()
        {
            // Arrange  
            var internalEmployee = new InternalEmployee(
                "Brooklyn", "Cannon", 5, 3000, false, 1);

            // Act 
            await _employeeServiceFixture.EmployeeService
                .GiveRaiseAsync(internalEmployee, 200);

            // Assert
            Assert.False(internalEmployee.MinimumRaiseGiven);
        }
    }
}
```

## How to Run Tests

1. Open the solution in Visual Studio.
2. Build the solution to restore all dependencies.
3. Open the Test Explorer (`Test` > `Test Explorer`).
4. Run all tests to ensure everything is working correctly.

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.
