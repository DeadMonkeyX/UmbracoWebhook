using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;
using UmbracoWebhook.Database.Migrations;

namespace UmbracoWebhook.Notifications;

public class RunWebhooksMigration : INotificationHandler<UmbracoApplicationStartingNotification>
{
    private readonly ICoreScopeProvider _coreScopeProvider;
    private readonly IKeyValueService _keyValueService;
    private readonly IMigrationPlanExecutor _migrationPlanExecutor;
    private readonly IRuntimeState _runtimeState;

    public RunWebhooksMigration(IMigrationPlanExecutor migrationPlanExecutor, ICoreScopeProvider coreScopeProvider,
        IKeyValueService keyValueService, IRuntimeState runtimeState)
    {
        _migrationPlanExecutor = migrationPlanExecutor;
        _coreScopeProvider = coreScopeProvider;
        _keyValueService = keyValueService;
        _runtimeState = runtimeState;
    }

    public void Handle(UmbracoApplicationStartingNotification notification)
    {
        if (_runtimeState.Level < RuntimeLevel.Run) return;

        var migrationPlan = new MigrationPlan("Webhooks");

        migrationPlan.From(string.Empty)
            .To<AddWebhooksTable>("webhooks-table-initial");

        var upgrader = new Upgrader(migrationPlan);
        upgrader.Execute(
            _migrationPlanExecutor,
            _coreScopeProvider,
            _keyValueService);
    }
}