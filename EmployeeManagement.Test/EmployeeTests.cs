using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
    public class EmployeeTests
    {
        /// <summary>
        /// Fullname is concatenation of first and last name.
        /// </summary>
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameIsConcatenation()
        {
            //Arrange
            var employee = new 
                InternalEmployee("John", "Doe", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Lucia";
            employee.LastName = "Shelton";

            //Assert
            Assert.Equal($@"{employee.FirstName} {employee.LastName}", employee.FullName, ignoreCase:true);
        }

        /// <summary>
        /// Fullname starts with first name.
        /// </summary>
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameStartsWithFirstName()
        {
            //Arrange
            var employee = new
                InternalEmployee("John", "Doe", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Lucia";
            employee.LastName = "Shelton";

            //Assert
            Assert.StartsWith(employee.FirstName, employee.FullName);
        }

        /// <summary>
        /// Fullname starts with first name.
        /// </summary>
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameEndsWithLastName()
        {
            //Arrange
            var employee = new
                InternalEmployee("John", "Doe", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Lucia";
            employee.LastName = "Shelton";

            //Assert
            Assert.EndsWith(employee.LastName, employee.FullName);
        }

        /// <summary>
        /// Fullname contains part of concatenation.
        /// </summary>
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameContainsPartOfConcatenation()
        {
            //Arrange
            var employee = new
                InternalEmployee("John", "Doe", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Lucia";
            employee.LastName = "Shelton";

            //Assert
            Assert.Contains("ia Sh", employee.FullName);
        }

        /// <summary>
        /// Fullname sounds like concatenation.
        /// </summary>
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameSoundsLikeConcatenation()
        {
            //Arrange
            var employee = new
                InternalEmployee("John", "Doe", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Lucia";
            employee.LastName = "Shelton";

            //Assert
            Assert.Matches("Lu(c|s|z)ia Shel(t|d)on", employee.FullName);
        }
    }
}
