using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Notifications;
using UmbracoWebhook.Database.Repositories;
using UmbracoWebhook.Database.Repositories.Interfaces;
using UmbracoWebhook.Notifications;
using UmbracoWebhook.Services;

namespace UmbracoWebhook.Compositions;

public class WebhooksComposition : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddNotificationHandler<UmbracoApplicationStartingNotification, RunWebhooksMigration>();
        builder.Services.AddTransient<IWebhookRepository, WebhookRepository>();
        builder.Services.AddTransient<WebhookService>();
        builder.AddNotificationHandler<ContentPublishedNotification, PublishedContentHandler>();
    }
}