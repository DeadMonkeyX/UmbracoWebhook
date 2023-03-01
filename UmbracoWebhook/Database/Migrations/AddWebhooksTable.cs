using Umbraco.Cms.Infrastructure.Migrations;

namespace UmbracoWebhook.Database.Migrations;

public class AddWebhooksTable : MigrationBase
{
    public AddWebhooksTable(IMigrationContext context) : base(context)
    {
    }

    protected override void Migrate()
    {
        Logger.LogDebug("Running migration {MigrationStep}", "AddWebhooksTable");
        if (TableExists("Webhooks") is false)
            Create.Table<Webhook>().Do();
        else
            Logger.LogDebug("The database table {DbTable} already exists, skipping", "AddWebhooksTable");
    }
}