using PocketMoney.Adapters;
using PocketMoney.Integrations.MessageBroker;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Interfaces;
using PocketMoney.Models.Accounting;
using PocketMoney.Models.Accounting.Responses;
using PocketMoney.Models.Classification.Requests;
using PocketMoney.Services;

namespace PocketMoney.IoC;

public class ServiceInjection
{
    public static void Inject(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDataContextFactory, DataContextFactory>();
        serviceCollection.AddTransient<IMessageBrokerContext, MessageBrokerContext>();
        serviceCollection.AddSingleton<IProductService, ProductService>();
        serviceCollection.AddSingleton<IAccountService, AccountService>();
        serviceCollection.AddSingleton<ITransactionService, TransactionService>();
        serviceCollection.AddSingleton<IVendorService, VendorService>();
        serviceCollection.AddSingleton<ICategoryService, CategoryService>();
        serviceCollection.AddSingleton<ICategoryService, CategoryService>();

        serviceCollection.AddTransient<IAdapter<TransactionResponse, Transaction>, TransactionAdapter>();
    }
}