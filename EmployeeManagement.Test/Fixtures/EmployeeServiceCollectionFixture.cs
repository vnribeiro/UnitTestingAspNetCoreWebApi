namespace EmployeeManagement.Test.Fixtures
{
    /// <summary>
    /// Defines a collection fixture for the EmployeeService tests.
    /// This class is used to share context and dependencies across multiple test classes.
    /// The collection fixture ensures that the setup and teardown logic in the 
    /// <see cref="EmployeeServiceFixture"/> is executed once for all tests in the collection.
    /// </summary>
    [CollectionDefinition("EmployeeServiceCollection")]
    public class EmployeeServiceCollectionFixture 
        : ICollectionFixture<EmployeeServiceFixture> { }
}
