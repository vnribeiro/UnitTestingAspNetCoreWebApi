using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;
using EmployeeManagement.Test.TestData;

namespace EmployeeManagement.Test
{
    [Collection("EmployeeServiceCollection")]
    public class DataDriveEmployeeServiceTests //: IClassFixture<EmployeeServiceFixture>
    {
        private readonly EmployeeServiceFixture _employeeServiceFixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeServiceTests"/> class.
        /// This constructor is used for the class fixture approach, which allows sharing
        /// setup and cleanup code across multiple test methods.
        /// </summary>
        /// <param name="employeeServiceFixture">
        /// An instance of <see cref="EmployeeServiceFixture"/> that provides the necessary
        /// setup and dependencies for the tests.
        /// </param>
        public DataDriveEmployeeServiceTests(EmployeeServiceFixture employeeServiceFixture)
        {
            _employeeServiceFixture = employeeServiceFixture;
        }

        /// <summary>
        /// Employee must have attended First and Second obligatory course.
        /// Theory with InlineData attribute.
        /// </summary>
        [Theory]
        [InlineData("37e03ca7-c730-4351-834c-b66f280cdb01")]
        [InlineData("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e")]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstAndSecondObligatoryCourse(Guid courseId)
        {
            //Arrange

            //Act
            var employee = _employeeServiceFixture
                .EmployeeService
                .CreateInternalEmployee("John", "Doe");

            //Assert With Predicate
            Assert.Contains(employee.AttendedCourses,
                course => course.Id == courseId);
        }

        /// <summary>
        /// Tests that the minimum raise given to an employee is correctly flagged as true.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
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

        /// <summary>
        /// Tests that the minimum raise given to an employee is correctly flagged as false.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
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

        /// <summary>
        /// Example test data for GiveRaise with property.
        /// </summary>
        public static IEnumerable<object[]> ExampleTestDataForGiveRaiseWithProperty
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { 100, true },
                    new object[] { 200, false }
                };
            }
        }

        /// <summary>
        /// Example test data for GiveRaise with Method.
        /// </summary>
        public static IEnumerable<object[]> ExampleTestDataForGiveRaiseWithMethod(int testDataInstancesToProvide)
        {
            var testData = new List<object[]>
            {
                new object[] { 100, true },
                new object[] { 200, false }
            };

            return testData.Take(testDataInstancesToProvide);
        }

        /// <summary>
        /// Employee minimum raise given must match the expected value.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Theory]
        [MemberData(nameof(ExampleTestDataForGiveRaiseWithProperty))]
        public async Task GiveRaise_RaiseGiven_EmployeeMinimumRaiseGivenMatchesValue
            (int raiseGiven, bool expectedValueForMinimumRaiseGiven)
        {
            // Arrange  
            var internalEmployee = new InternalEmployee(
                "Brooklyn", "Cannon", 5, 3000, false, 1);

            // Act
            await _employeeServiceFixture
                .EmployeeService.GiveRaiseAsync(internalEmployee, raiseGiven);

            // Assert
            Assert.Equal(expectedValueForMinimumRaiseGiven, 
                internalEmployee.MinimumRaiseGiven);
        }

        /// <summary>
        /// Employee minimum raise given must match the expected value.
        /// This is an example how you can share test data between different test methods in
        /// different test classes.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Theory]
        [MemberData(nameof(DataDriveEmployeeServiceTests.ExampleTestDataForGiveRaiseWithMethod), 
            1, 
            MemberType = typeof(DataDriveEmployeeServiceTests))]
        public async Task GiveRaise_RaiseGiven_EmployeeMinimumRaiseGivenMatchesValue_Shared
            (int raiseGiven, bool expectedValueForMinimumRaiseGiven)
        {
            // Arrange  
            var internalEmployee = new InternalEmployee(
                "Brooklyn", "Cannon", 5, 3000, false, 1);

            // Act
            await _employeeServiceFixture
                .EmployeeService.GiveRaiseAsync(internalEmployee, raiseGiven);

            // Assert
            Assert.Equal(expectedValueForMinimumRaiseGiven,
                internalEmployee.MinimumRaiseGiven);
        }

        /// <summary>
        /// Example test data for GiveRaise with property. (StronglyTyped)
        /// </summary>
        public static TheoryData<int, bool> StronglyTypedExampleTestDataForGiveRaiseWithProperty
        {
            get
            {
                return new TheoryData<int, bool>
                {
                    { 100, true },
                    { 200, false }
                };
            }
        }

        /// <summary>
        /// Employee minimum raise given must match the expected value.
        /// This is an example how you can use strongly typed test data with TheoryData.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Theory]
        [MemberData(nameof(StronglyTypedExampleTestDataForGiveRaiseWithProperty))]
        public async Task GiveRaise_RaiseGiven_EmployeeMinimumRaiseGivenMatchesValue_WithMemberData_UsingTheoryData
            (int raiseGiven, bool expectedValueForMinimumRaiseGiven)
        {
            // Arrange  
            var internalEmployee = new InternalEmployee(
                "Brooklyn", "Cannon", 5, 3000, false, 1);

            // Act
            await _employeeServiceFixture
                .EmployeeService.GiveRaiseAsync(internalEmployee, raiseGiven);

            // Assert
            Assert.Equal(expectedValueForMinimumRaiseGiven,
                internalEmployee.MinimumRaiseGiven);
        }

        /// <summary>
        /// Employee minimum raise given must match the expected value.
        /// this method is an example of using a class data attribute to provide test data.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Theory]
        [ClassData(typeof(EmployeeServiceTestData))]
        public async Task GiveRaise_RaiseGiven_EmployeeMinimumRaiseGivenMatchesValue_WithClassData
            (int raiseGiven, bool expectedValueForMinimumRaiseGiven)
        {
            // Arrange  
            var internalEmployee = new InternalEmployee(
                "Brooklyn", "Cannon", 5, 3000, false, 1);

            // Act
            await _employeeServiceFixture
                .EmployeeService.GiveRaiseAsync(internalEmployee, raiseGiven);

            // Assert
            Assert.Equal(expectedValueForMinimumRaiseGiven,
                internalEmployee.MinimumRaiseGiven);
        }

        /// <summary>
        /// Employee minimum raise given must match the expected value.
        /// this method is an example of using a class data attribute to provide test data.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Theory]
        [ClassData(typeof(StronglyTypedEmployeeServiceTestData))]
        public async Task GiveRaise_RaiseGiven_EmployeeMinimumRaiseGivenMatchesValue_WithTheoryData
            (int raiseGiven, bool expectedValueForMinimumRaiseGiven)
        {
            // Arrange  
            var internalEmployee = new InternalEmployee(
                "Brooklyn", "Cannon", 5, 3000, false, 1);

            // Act
            await _employeeServiceFixture
                .EmployeeService.GiveRaiseAsync(internalEmployee, raiseGiven);

            // Assert
            Assert.Equal(expectedValueForMinimumRaiseGiven,
                internalEmployee.MinimumRaiseGiven);
        }

        /// <summary>
        /// Employee minimum raise given must match the expected value.
        /// This method is an example of using a class data attribute to provide test data from a file.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Theory]
        [ClassData(typeof(StronglyTypedEmployeeServiceTestDataFromFile))]
        public async Task GiveRaise_RaiseGiven_EmployeeMinimumRaiseGivenMatchesValue_WithTheoryData_UsingDataFromFile
            (int raiseGiven, bool expectedValueForMinimumRaiseGiven)
        {
            // Arrange  
            var internalEmployee = new InternalEmployee(
                "Brooklyn", "Cannon", 5, 3000, false, 1);

            // Act
            await _employeeServiceFixture
                .EmployeeService.GiveRaiseAsync(internalEmployee, raiseGiven);

            // Assert
            Assert.Equal(expectedValueForMinimumRaiseGiven,
                internalEmployee.MinimumRaiseGiven);
        }
    }
}
