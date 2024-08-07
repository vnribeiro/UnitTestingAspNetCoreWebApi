using EmployeeManagement.Business;
using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;
using EmployeeManagement.Test.Services;
using Xunit.Abstractions;

namespace EmployeeManagement.Test
{
    [Collection("EmployeeServiceCollection")]
    public class EmployeeServiceTests //: IClassFixture<EmployeeServiceFixture>
    {
        private readonly EmployeeServiceFixture _employeeServiceFixture;
        private readonly ITestOutputHelper _testOutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeServiceTests"/> class.
        /// This constructor is used for the class fixture approach, which allows sharing
        /// setup and cleanup code across multiple test methods.
        /// </summary>
        /// <param name="employeeServiceFixture">
        /// An instance of <see cref="EmployeeServiceFixture"/> that provides the necessary
        /// setup and dependencies for the tests.
        /// </param>
        /// <param name="testOutputHelper">
        /// An instance of <see cref="ITestOutputHelper"/> that provides the necessary
        /// implementation for writing output to test results.
        /// </param>
        public EmployeeServiceTests(EmployeeServiceFixture employeeServiceFixture, 
            ITestOutputHelper testOutputHelper)
        {
            _employeeServiceFixture = employeeServiceFixture;
            _testOutputHelper = testOutputHelper;
        }

        /// <summary>
        /// Employee must have attended first obligatory course.
        /// </summary>
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse()
        {
            //Arrange
            var obligatoryCourse =
                _employeeServiceFixture
                    .EmployeeManagementTestDataRepository
                    .GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            //Act
            var internalEmployee = _employeeServiceFixture
                .EmployeeService
                .CreateInternalEmployee("John", "Doe");

            //Write output to test results
            // The result will be displayed in the test explorer.
            _testOutputHelper
                .WriteLine($"Employee after Act: {internalEmployee.FirstName} {internalEmployee.LastName}");

            internalEmployee
                .AttendedCourses
                .ForEach(c => _testOutputHelper.WriteLine($"Attended course: {c.Id} {c.Title}"));

            //Assert
            Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses);
        }

        /// <summary>
        /// Employee must have attended first obligatory course. (with Object)
        /// </summary>
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithObject()
        {
            //Arrange

            var obligatoryCourse =
                _employeeServiceFixture
                    .EmployeeManagementTestDataRepository
                    .GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            //Act
            var employee = _employeeServiceFixture
                .EmployeeService
                .CreateInternalEmployee("John", "Doe");

            //Assert With Object
            Assert.Contains(obligatoryCourse, employee.AttendedCourses);
        }

        /// <summary>
        /// Employee must have attended first obligatory course. (with Predicate)
        /// </summary>
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithPredicate()
        {
            //Arrange

            //Act
            var employee = _employeeServiceFixture
                .EmployeeService
                .CreateInternalEmployee("John", "Doe");

            //Assert With Predicate
            Assert.Contains(employee.AttendedCourses, 
                course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
        }

        /// <summary>
        /// Employee must have attended Second obligatory course. (with Predicate)
        /// </summary>
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedSecondObligatoryCourse_WithPredicate()
        {
            //Arrange

            //Act
            var employee = _employeeServiceFixture
                .EmployeeService
                .CreateInternalEmployee("John", "Doe");

            //Assert With Predicate
            Assert.Contains(employee.AttendedCourses,
                course => course.Id == Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));
        }

        /// <summary>
        /// Employee Attended Courses must match obligatory courses.
        /// </summary>
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses()
        {
            //Arrange

            var obligatoryCourses =
                _employeeServiceFixture
                    .EmployeeManagementTestDataRepository.GetCourses(
                    Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                    Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            //Act
            var employee = _employeeServiceFixture
                .EmployeeService
                .CreateInternalEmployee("John", "Doe");

            //Assert
            Assert.Equal(obligatoryCourses, employee.AttendedCourses);
        }

        /// <summary>
        /// employee courses must not be new.
        /// </summary>
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustNotBeNew()
        {
            //Arrange

            //Act
            var employee = _employeeServiceFixture
                .EmployeeService
                .CreateInternalEmployee("John", "Doe");

            //Assert
            Assert.All(employee.AttendedCourses,
                course => Assert.False(course.IsNew));
        }

        /// <summary>
        /// Employee Attended Courses must match obligatory courses. (Async)
        /// </summary>
        [Fact]
        public async Task CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses_Async()
        {
            //Arrange

            var obligatoryCourses = await _employeeServiceFixture
                .EmployeeManagementTestDataRepository.GetCoursesAsync(
                    Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                    Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            //Act
            var employee = await _employeeServiceFixture
                .EmployeeService
                .CreateInternalEmployeeAsync("John", "Doe");

            //Assert
            Assert.Equal(obligatoryCourses, employee.AttendedCourses);
        }

        /// <summary>
        /// Employee invalid raise exceptions must be thrown.
        /// </summary>
        [Fact]
        public async Task GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionsMustBeThrown()
        {
            //Arrange
            var internalEmployee =
                new InternalEmployee("John", "Doe", 5, 3000, false, 1);

            //Act and Assert
            await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
                async () => await _employeeServiceFixture
                    .EmployeeService
                    .GiveRaiseAsync(internalEmployee, 50));
        }

        ///// <summary>
        ///// Employee invalid raise exceptions must be thrown. (Mistake)
        ///// This test is incorrect.
        ///// </summary>
        //[Fact]
        //public void GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionsMustBeThrown_Mistake()
        //{
        //    //Arrange
        //    var internalEmployee =
        //        new InternalEmployee("John", "Doe", 5, 3000, false, 1);

        //    //Act and Assert
        //    Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
        //        async () => await _employeeServiceFixture
        //            .EmployeeService
        //            .GiveRaiseAsync(internalEmployee, 50));
        //}

        /// <summary>
        /// Employee is absent must be triggered. (Asserting with Events)
        /// </summary>
        [Fact]
        public void NotifyOfAbsence_EmployeeIsAbsent_OnEmployeeIsAbsentMustBeTriggered()
        {
            //Arrange
            var internalEmployee =
                new InternalEmployee("John", "Doe", 5, 3000, false, 1);

            //Act and Assert
             Assert.Raises<EmployeeIsAbsentEventArgs>(
                handler => _employeeServiceFixture
                    .EmployeeService.EmployeeIsAbsent += handler,
                handler => _employeeServiceFixture
                    .EmployeeService.EmployeeIsAbsent -= handler,
                () => _employeeServiceFixture
                    .EmployeeService.NotifyOfAbsence(internalEmployee));
        }
    }
}
