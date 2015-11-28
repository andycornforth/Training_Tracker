using Repository;
using System.Data;

namespace RepositoryTests
{
    public class IntegrationTestData : BaseSqlRepository
    {
        public static object LockObject = new object();
        public static bool DataInitialized = false;

        public IntegrationTestData()
            : base()
        {

        }

        public void SetUp()
        {
            lock (LockObject)
            {
                if (DataInitialized)
                {
                    return;
                }

                // Clean up
                DeleteData();

                // Set up
                DeleteData();

                DataInitialized = true;
            }

        }

        public void CleanUp()
        {
            DeleteData();
            DataInitialized = false;
        }

        private void DeleteData()
        {
            var command = GetCommand(@"DELETE FROM dbo.Log 
                                       DELETE FROM dbo.Person", CommandType.Text);

            ExecuteNonQuery(command);
        }
    }
}
