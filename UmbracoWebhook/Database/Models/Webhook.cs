using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace UmbracoWebhook.Database;

[TableName("Webhooks")]
[PrimaryKey("Id", AutoIncrement = true)]
[ExplicitColumns]
public class Webhook
{
    [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Url")] public string Url { get; set; }
    [Column("ContentTypeAlias")] public string ContentTypeAlias { get; set; }
}