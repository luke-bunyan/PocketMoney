using MySqlConnector;
using PocketMoney.Integrations.Persistance;

namespace PocketMoney.IoC;

public class DataServiceInjection
{
    public static void Inject(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MySqlConnection>();
        var instances = System.Reflection.Assembly.GetExecutingAssembly()
            .GetTypes().Where(item => item.GetInterfaces().Where(i => i.IsGenericType).Any(i =>
                i.GetGenericTypeDefinition() == typeof(IDataContext<>) &&
                !item.IsAbstract && !item.IsInterface))
            .ToList();

        foreach (var instance in instances)
        {
            if(instance.Name == "GenericData`1") continue;
            var definition = instance.GetInterfaces().First(item => item.IsGenericType && item.GetGenericTypeDefinition() == typeof(IDataContext<>));
            serviceCollection.AddSingleton(definition, instance);
        }
    }
}