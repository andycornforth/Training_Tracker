using Microsoft.Practices.Unity;
using Repository;

namespace Business
{
    public class BusinessUnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IPersonRepository, PersonRepository>();
            container.RegisterType<ILogRepository, LogRepository>();
        }
    }
}
