using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Test.Services;
using Moq;

namespace EmployeeManagement.Test
{
    public class MoqTests
    {
        /// <summary>
        /// method that uses Moq to mock the EmployeeFactory
        /// </summary>
        [Fact]
        public void FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated()
        {
            // Arrange
            var employeeManagementTestDataRepository =
                new EmployeeManagementTestDataRepository();
            //var employeeFactory = new EmployeeFactory();

            //mocking the EmployeeFactory
            var employeeFactoryMock = new Mock<EmployeeFactory>();

            var employeeService = new EmployeeService(
                employeeManagementTestDataRepository,
                employeeFactoryMock.Object);

            // Act 
            var employee = employeeService.FetchInternalEmployee(
                Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));

            // Assert  
            Assert.Equal(400, employee!.SuggestedBonus);
        }

        /// <summary>
        /// This is an example of method that uses Moq to mock the EmployeeFactory
        /// </summary>
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_SuggestedBonusMustBeCalculated()
        {
            // Arrange
            var employeeManagementTestDataRepository =
                new EmployeeManagementTestDataRepository();

            //mocking the EmployeeFactory
            var employeeFactoryMock = new Mock<EmployeeFactory>();

            // mocking the CreateEmployee method
            // this line will generate an error because the name of the employee
            // is not the same as the one in the test
            employeeFactoryMock
                .Setup(x => x.CreateEmployee("Kevin", It.IsAny<string>(), null, false))
                .Returns(new InternalEmployee("Kevin", "Dock", 5, 2500, false, 1));

            // return a new InternalEmployee object with the name "Sandy"
            // that matches the firstName used in the test
            employeeFactoryMock
                .Setup(x => x.CreateEmployee("Sandy", It.IsAny<string>(), null, false))
                .Returns(new InternalEmployee("Sandy", "Dock", 5, 2500, false, 1));

            // return a new InternalEmployee object with the name "SomeoneWithAna"
            // that contains the letter "a" in the name
            // when two potential matches are possible the last defined mock will be used
            employeeFactoryMock
                .Setup(x => 
                    x.CreateEmployee(It.Is<string>(value => value.Contains("a")), 
                    It.IsAny<string>(), 
                    null, 
                    false))
                .Returns(new InternalEmployee("SomeoneWithAna", "Dock", 5, 2500, false, 1));


            var employeeService = new EmployeeService(
                employeeManagementTestDataRepository,
                employeeFactoryMock.Object);

            // suggested bonus for new employees
            // (years in service if > 0) * attended courses * 100
            const decimal suggestedBonus = 1000;

            // Act 
            var employee = employeeService.CreateInternalEmployee("Sandy", "Dock");

            // Assert  
            Assert.Equal(suggestedBonus, employee!.SuggestedBonus);
        }

        /// <summary>
        /// This is an example of method that uses Moq to mock interfaces
        /// </summary>
        [Fact]
        public void FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated_MoqInterface()
        {
            // Arrange

            // create a Mock of the IEmployeeManagementRepository interface
            var employeeManagementTestDataRepositoryMock = 
                new Mock<IEmployeeManagementRepository>();

            // Setup the GetInternalEmployee method to return a new InternalEmployee object
            employeeManagementTestDataRepositoryMock
                    .Setup(x=>x.GetInternalEmployee(It.IsAny<Guid>()))
                    .Returns(new InternalEmployee("Tony", "Hall", 2, 2500, false, 2)
                    {
                        AttendedCourses =
                        [
                            new Course("Course one"),
                            new Course("Course Two")
                        ]
                    });

            //create a Mock of the EmployeeFactory interface
            var employeeFactoryMock = new Mock<EmployeeFactory>();

            var employeeService = new EmployeeService(
                employeeManagementTestDataRepositoryMock.Object,
                employeeFactoryMock.Object);

            // Act 
            var employee = employeeService.FetchInternalEmployee(
                Guid.Empty);

            // Assert  
            Assert.Equal(400, employee!.SuggestedBonus);
        }

        /// <summary>
        /// This is an example of method that uses Moq to mock interfaces with async methods
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated_MoqInterface_Async()
        {
            // Arrange
            var employeeManagementTestDataRepositoryMock =
                new Mock<IEmployeeManagementRepository>();

            // Setup the GetInternalEmployeeAsync method to return a new InternalEmployee object
            employeeManagementTestDataRepositoryMock
                .Setup(m => m.GetInternalEmployeeAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new InternalEmployee("Tony", "Hall", 2, 2500, false, 2)
                {
                    AttendedCourses = [
                        new Course("A course"), 
                        new Course("Another course")
                    ]
                });

            var employeeFactoryMock = new Mock<EmployeeFactory>();
            var employeeService = new EmployeeService(
                employeeManagementTestDataRepositoryMock.Object,
                employeeFactoryMock.Object);

            // Act 
            var employee = await employeeService.FetchInternalEmployeeAsync(Guid.Empty);

            // Assert  
            Assert.Equal(400, employee!.SuggestedBonus);
        }
    }
}
