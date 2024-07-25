using MySqlConnector;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Models.Accounting;

namespace PocketMoney.Data;

public class TransactionSqlData(MySqlConnection context, IConfiguration configuration) : BaseSqlDataContext<Transaction>(context,
    configuration,
    schemaName: "PocketMoney",
    database: "Transactions",
    keyColumnName: "TransactionId"), IDataContext<Transaction>;