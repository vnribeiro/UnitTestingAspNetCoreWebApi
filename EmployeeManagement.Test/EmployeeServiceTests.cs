using EmployeeManagement.Business;
using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Services;

namespace EmployeeManagement.Test
{
    public class EmployeeServiceTests
    {
        /// <summary>
        /// Employee must have attended first obligatory course.
        /// </summary>
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse()
        {
            //Arrange
            var employeeManagementTestDataRepository = new 
                EmployeeManagementTestDataRepository();

            var employeeService = new 
                EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

            var obligatoryCourse = 
                employeeManagementTestDataRepository.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            //Act
            var employee = employeeService.CreateInternalEmployee("John", "Doe");

            //Assert
            Assert.Contains(obligatoryCourse, employee.AttendedCourses);
        }

        /// <summary>
        /// Employee must have attended first obligatory course. (with Object)
        /// </summary>
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithObject()
        {
            //Arrange
            var employeeManagementTestDataRepository = new
                EmployeeManagementTestDataRepository();

            var employeeService = new
                EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

            var obligatoryCourse =
                employeeManagementTestDataRepository.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            //Act
            var employee = employeeService.CreateInternalEmployee("John", "Doe");

            //Assert
            Assert.Contains(obligatoryCourse, employee.AttendedCourses);
        }

        /// <summary>
        /// Employee must have attended first obligatory course. (with Predicate)
        /// </summary>
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithPredicate()
        {
            //Arrange
            var employeeManagementTestDataRepository = new
                EmployeeManagementTestDataRepository();

            var employeeService = new
                EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

            //Act
            var employee = employeeService.CreateInternalEmployee("John", "Doe");

            //Assert
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
            var employeeManagementTestDataRepository = new
                EmployeeManagementTestDataRepository();

            var employeeService = new
                EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

            //Act
            var employee = employeeService.CreateInternalEmployee("John", "Doe");

            //Assert
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
            var employeeManagementTestDataRepository = new
                EmployeeManagementTestDataRepository();

            var employeeService = new
                EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

            var obligatoryCourses = 
                employeeManagementTestDataRepository.GetCourses(
                    Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                    Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            //Act
            var employee = employeeService.CreateInternalEmployee("John", "Doe");

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
            var employeeManagementTestDataRepository = new
                EmployeeManagementTestDataRepository();

            var employeeService = new
                EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

            //Act
            var employee = employeeService.CreateInternalEmployee("John", "Doe");

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
            var employeeManagementTestDataRepository = new
                EmployeeManagementTestDataRepository();

            var employeeService = new
                EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());

            var obligatoryCourses = await employeeManagementTestDataRepository
                .GetCoursesAsync(
                    Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                    Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            //Act
            var employee = await employeeService.CreateInternalEmployeeAsync("John", "Doe");

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
            var employeeService = new EmployeeService(
                new EmployeeManagementTestDataRepository(), 
                new EmployeeFactory());

            var internalEmployee =
                new InternalEmployee("John", "Doe", 5, 3000, false, 1);

            //Act and Assert
            await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
                async () => await employeeService.GiveRaiseAsync(internalEmployee, 50));
        }

        ///// <summary>
        ///// Employee invalid raise exceptions must be thrown. (Mistake)
        ///// This test is incorrect.
        ///// </summary>
        //[Fact]
        //public void GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionsMustBeThrown_Mistake()
        //{
        //    //Arrange
        //    var employeeService = new EmployeeService(
        //        new EmployeeManagementTestDataRepository(),
        //        new EmployeeFactory());

        //    var internalEmployee =
        //        new InternalEmployee("John", "Doe", 5, 3000, false, 1);

        //    //Act and Assert
        //    Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
        //        async () => await employeeService.GiveRaiseAsync(internalEmployee, 50));
        //}

        /// <summary>
        /// Employee is absent must be triggered. (Asserting with Events)
        /// </summary>
        [Fact]
        public void NotifyOfAbsence_EmployeeIsAbsent_OnEmployeeIsAbsentMustBeTriggered()
        {
            //Arrange
            var employeeService = new EmployeeService(
                new EmployeeManagementTestDataRepository(),
                new EmployeeFactory());

            var internalEmployee =
                new InternalEmployee("John", "Doe", 5, 3000, false, 1);

            //Act and Assert
             Assert.Raises<EmployeeIsAbsentEventArgs>(
                handler => employeeService.EmployeeIsAbsent += handler,
                handler => employeeService.EmployeeIsAbsent -= handler,
                () => employeeService.NotifyOfAbsence(internalEmployee));
        }
    }
}
