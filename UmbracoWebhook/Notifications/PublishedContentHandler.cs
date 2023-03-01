using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using UmbracoWebhook.Services;

namespace UmbracoWebhook.Notifications;

public class PublishedContentHandler : INotificationHandler<ContentPublishedNotification>
{
    private readonly WebhookService _webhookService;

    public PublishedContentHandler(WebhookService webhookService)
    {
        _webhookService = webhookService;
    }

    public void Handle(ContentPublishedNotification notification)
    {
        Task.Run(() => _webhookService.CallWebhooksAsync(notification.PublishedEntities));
    }
}