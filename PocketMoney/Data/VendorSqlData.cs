using MySqlConnector;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Models.Accounting;

namespace PocketMoney.Data;

public class VendorSqlData(MySqlConnection context, IConfiguration configuration) : BaseSqlDataContext<Vendor>(context,
    configuration,
    schemaName: "PocketMoney",
    database: "Vendors",
    keyColumnName: "VendorId"), IDataContext<Vendor>;