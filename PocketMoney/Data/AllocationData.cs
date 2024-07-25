using MySqlConnector;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Models.Accounting;

namespace PocketMoney.Data;

public class AllocationData(MySqlConnection context, IConfiguration configuration) : BaseDataContext<Allocation>(context,
    configuration,
    schemaName: "PocketMoney",
    database: "Allocations",
    keyColumnName: "AllocationId"), IDataContext<Allocation>;