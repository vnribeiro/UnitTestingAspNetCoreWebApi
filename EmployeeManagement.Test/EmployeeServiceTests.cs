using EmployeeManagement.Business;
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
        /// Employee Attended Courses must match obligatory courses.
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
    }
}
