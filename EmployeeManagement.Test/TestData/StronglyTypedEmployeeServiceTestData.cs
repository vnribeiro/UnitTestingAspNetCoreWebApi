namespace EmployeeManagement.Test.TestData
{
    public class StronglyTypedEmployeeServiceTestData : TheoryData<int, bool>
    {
        /// <summary>
        /// test data for EmployeeServiceTestData
        /// </summary>
        public StronglyTypedEmployeeServiceTestData()
        {
            Add(100, true);
            Add(200, false);
        }
    }
}
