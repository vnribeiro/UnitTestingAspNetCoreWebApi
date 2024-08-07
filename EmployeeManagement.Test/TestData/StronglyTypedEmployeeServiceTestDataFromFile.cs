namespace EmployeeManagement.Test.TestData
{
    public class StronglyTypedEmployeeServiceTestDataFromFile : TheoryData<int, bool>
    {
        private const string FilePath = "TestData/EmployeeServiceTestData.csv";

        /// <summary>
        /// read the data from the file and add it to the test data.
        /// </summary>
        public StronglyTypedEmployeeServiceTestDataFromFile()
        {
            var dataLines = File.ReadAllLines(FilePath);

            foreach (var line in dataLines)
            {
                //split the string by comma
                var splitString = line.Split(',');

                //try to parse the string to int and bool
                if (int.TryParse(splitString[0], out int raise) && 
                    bool.TryParse(splitString[1], out bool minimumRaiseGiven))
                {
                    //add test data
                    Add(raise, minimumRaiseGiven);
                }
            }
        }
    }
}
