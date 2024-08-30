namespace SSF.EFCore.AuditLogs;
public class ExternalAuditLogSqlOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public bool AutoCreateSqlTable { get; set; } = true;
    public string EntityTableName { get; set; } = "EntityAuditLogs";
    public string PropertyTableName { get; set; } = "PropertyChangeLogs";
    public string SchemaName { get; set; } = "dbo";
}


