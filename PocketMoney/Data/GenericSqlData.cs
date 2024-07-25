using MySqlConnector;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Models;

namespace PocketMoney.Data;

public class GenericSqlData<T>(MySqlConnection context, IConfiguration configuration, string schemaName, string database, string keyColumnName) : BaseSqlDataContext<T>(context,
    configuration,
    schemaName: schemaName,
    database: database,
    keyColumnName: keyColumnName), IDataContext<T>;