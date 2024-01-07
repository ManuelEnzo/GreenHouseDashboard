using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseDashboard.DI
{
    public class ServiceContainer
    {
        private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        public static void RegisterService<T>(T service)
        {
            Services[typeof(T)] = service;
        }

        public static T GetService<T>()
        {
            if (Services.TryGetValue(typeof(T), out var service))
                return (T)service;
            else
                return default(T);
        }
    }

}
