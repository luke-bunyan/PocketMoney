using MySqlConnector;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Models.Accounting;

namespace PocketMoney.Data;

public class TransactionData(MySqlConnection context, IConfiguration configuration) : BaseDataContext<Transaction>(context,
    configuration,
    schemaName: "PocketMoney",
    database: "Transactions",
    keyColumnName: "TransactionId"), IDataContext<Transaction>;