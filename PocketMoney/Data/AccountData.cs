using MySqlConnector;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Models.Accounting;

namespace PocketMoney.Data;

public class AccountData(MySqlConnection context, IConfiguration configuration) : BaseDataContext<Account>(context,
    configuration,
    schemaName: "PocketMoney",
    database: "Accounts",
    keyColumnName: "AccountId"), IDataContext<Account>;