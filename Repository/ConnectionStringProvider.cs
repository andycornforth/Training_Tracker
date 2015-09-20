using System.Configuration;

namespace Repository
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString();
    }
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["TrainingConnectionString"].ConnectionString;
        }
    }
}
