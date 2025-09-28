using MySqlConnector;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Models;

namespace PocketMoney.Data;

public class GenericSqlData<T>(MySqlConnection context, IConfiguration configuration, string database, string keyColumnName) : BaseSqlDataContext<T>(context,
    configuration,
    database: database,
    keyColumnName: keyColumnName), IDataContext<T>;