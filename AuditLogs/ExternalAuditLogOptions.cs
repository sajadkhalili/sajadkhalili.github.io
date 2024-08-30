namespace SSF.EFCore.AuditLogs;


public class ExternalAuditLogOptions
{
    public List<string> propertyForReject { get; set; } = new List<string>
            {
                "CreatedByUserId",
                "CreatedDateTime",
                "ModifiedByUserId",
                "ModifiedDateTime"
            };
    public string BusinessIdFieldName { get; set; } = "ExternalId";

}


