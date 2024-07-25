using MySqlConnector;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Models;

namespace PocketMoney.Data;

public class GenericData<T>(MySqlConnection context, IConfiguration configuration, string schemaName, string database, string keyColumnName) : BaseDataContext<T>(context,
    configuration,
    schemaName: schemaName,
    database: database,
    keyColumnName: keyColumnName), IDataContext<T>;