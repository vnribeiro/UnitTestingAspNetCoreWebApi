using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.DbContexts;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Test.HttpMessageHandlers;
using EmployeeManagement.Test.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;

namespace EmployeeManagement.Test
{
    public class TestIsolationApproachesTests
    {
        /// <summary>
        /// This method create an in-memory database and context, to use in the test.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="XunitException"></exception>
        [Fact]
        public async Task AttendCourseAsync_CourseAttended_SuggestedBonusMustCorrectlyBeRecalculated()
        {
            //range

            //create in-memory database and context
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseSqlite(connection);

            var dbContext = new EmployeeDbContext(optionsBuilder.Options);
            await dbContext.Database.MigrateAsync();

            var employeeManagementDataRepository = 
                new EmployeeManagementRepository(dbContext);

            var employeeService = new 
                EmployeeService(employeeManagementDataRepository, new EmployeeFactory());

            // end context creation

            //get course from database - "Dealing with customers 101"
            var courseToAttend = await employeeManagementDataRepository
                .GetCourseAsync(Guid.Parse("844e14ce-c055-49e9-9610-855669c9859b"));

            // get existing employee - "Megan Jones
            var internalEmployee = await employeeManagementDataRepository
                .GetInternalEmployeeAsync(Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));

            if(courseToAttend is null || internalEmployee is null){
                throw new XunitException("Arranging the test failed");
            }

            //expected suggested bonus after attending the course
            var exceptedSuggestedBonus = internalEmployee
                .YearsInService * (internalEmployee.AttendedCourses.Count + 1) * 100;

            //act
            await employeeService.AttendCourseAsync(internalEmployee, courseToAttend);

            //assert
            Assert.Equal(exceptedSuggestedBonus, internalEmployee.SuggestedBonus);
        }

        /// <summary>
        /// Promotion service test using http client with testable handler.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PromoteInternalEmployeeAsync_IsEligible_JobLevelMustBeIncreased()
        {
            //range
            var httpClient = 
                new HttpClient(new TestablePromotionEligibilityHandler(true));

            var internalEmployee = 
                new InternalEmployee("John", "Castle", 5, 3, false, 1);

            var promotionService = 
                new PromotionService(httpClient, new EmployeeManagementTestDataRepository());

            //act
            await promotionService.PromoteInternalEmployeeAsync(internalEmployee);

            //assert
            Assert.Equal(2, internalEmployee.JobLevel);
        }
    }
}
