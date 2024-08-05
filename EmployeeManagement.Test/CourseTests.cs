using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
    public class CourseTests
    {
        /// <summary>
        /// When a course is created, IsNew must be true.
        /// </summary>
        [Fact]
        public void CourseConstructor_ConstructCourse_IsNewMustBeTrue()
        {
            //Arrange
            //nothing to arrange

            //Act
            var course = new Course("Test Course");

            //Assert
            Assert.True(course.IsNew);
        }
    }
}
