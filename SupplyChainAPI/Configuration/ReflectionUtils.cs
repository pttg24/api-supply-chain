namespace SupplyChainAPI.Configuration
{
    using System.Linq;
    using System.Reflection;

    public class ReflectionUtils
    {
        public static string GetAssemblyVersion<T>() =>
            typeof(T).GetTypeInfo()
                .Assembly
                .GetCustomAttributes<AssemblyInformationalVersionAttribute>()
                .FirstOrDefault()?
                .InformationalVersion;
    }
}