using MySqlConnector;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Models.Accounting;

namespace PocketMoney.Data;

public class VendorData(MySqlConnection context, IConfiguration configuration) : BaseDataContext<Vendor>(context,
    configuration,
    schemaName: "PocketMoney",
    database: "Vendors",
    keyColumnName: "VendorId"), IDataContext<Vendor>;