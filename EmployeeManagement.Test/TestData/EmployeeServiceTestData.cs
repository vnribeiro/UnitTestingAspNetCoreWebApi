using System.Collections;

namespace EmployeeManagement.Test.TestData
{
    public class EmployeeServiceTestData : IEnumerable<object[]>
    {
        /// <summary>
        /// GetEnumerator method to return IEnumerator of array object.
        /// </summary>
        /// <returns>returns IEnumerator of array object</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 100, true };
            yield return new object[] { 200, false };
        }

        /// <summary>
        /// Return the Enumerator
        /// </summary>
        /// <returns>Returns the Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
