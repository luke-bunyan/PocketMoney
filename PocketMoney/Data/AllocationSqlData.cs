using MySqlConnector;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Models.Accounting;

namespace PocketMoney.Data;

public class AllocationSqlData(MySqlConnection context, IConfiguration configuration) : BaseSqlDataContext<Allocation>(context,
    configuration,
    schemaName: "PocketMoney",
    database: "Allocations",
    keyColumnName: "AllocationId"), IDataContext<Allocation>;