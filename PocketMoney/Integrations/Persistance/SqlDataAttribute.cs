namespace PocketMoney.Integrations.Persistance;

[AttributeUsage(AttributeTargets.Class)]
public class SqlDataAttribute(string database, string keyColumnName) : Attribute
{
    public string Database { get; set; } = database;

    public string KeyColumnName { get; set; } = keyColumnName;
}