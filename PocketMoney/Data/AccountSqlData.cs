using MySqlConnector;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Models.Accounting;

namespace PocketMoney.Data;

public class AccountSqlData(MySqlConnection context, IConfiguration configuration) : BaseSqlDataContext<Account>(context,
    configuration,
    schemaName: "PocketMoney",
    database: "Accounts",
    keyColumnName: "AccountId"), IDataContext<Account>;