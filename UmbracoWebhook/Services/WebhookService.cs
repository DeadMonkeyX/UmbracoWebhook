using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Web.Common;
using UmbracoWebhook.Database.Repositories.Interfaces;
using UmbracoWebhook.Models;

namespace UmbracoWebhook.Services;

public class WebhookService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly UmbracoHelper _umbracoHelper;
    private readonly IWebhookRepository _webhookRepository;

    public WebhookService(IWebhookRepository webhookRepository, IHttpClientFactory httpClientFactory,
        UmbracoHelper umbracoHelper)
    {
        _webhookRepository = webhookRepository;
        _httpClientFactory = httpClientFactory;
        _umbracoHelper = umbracoHelper;
    }

    private async Task<IEnumerable<PublishedWebhookData>> BuildWebhookData(IEnumerable<IContent> contentList)
    {
        var list = (from content in contentList
            let publishedContent = _umbracoHelper.Content(content.Id)
            where publishedContent is not null
            select new PublishedWebhookData
            {
                Id = content.Id, Name = content.Name, Url = publishedContent.Url(),
                PublishedDateTime = content.PublishDate, ContentTypeAlias = content.ContentType.Alias
            }).ToList();

        return list;
    }

    public async void CallWebhooksAsync(IEnumerable<IContent> contentList)
    {
        // Build data
        var data = await BuildWebhookData(contentList);

        // Get configured webhooks
        var webhooks = _webhookRepository.GetAll();

        // Create http client
        var client = _httpClientFactory.CreateClient();

        // Run post request async
        foreach (var webhook in webhooks)
        {
            // Process webhook that takes everything
            if (webhook.ContentTypeAlias is "")
            {
                await client.PostAsJsonAsync(new Uri(webhook.Url), data);
                continue;
            }

            // Filter data
            var filteredData = data.Where(x => webhook.ContentTypeAlias == x.ContentTypeAlias);
            if (!filteredData.Any())
                continue;

            await client.PostAsJsonAsync(new Uri(webhook.Url), filteredData);
        }
    }
}