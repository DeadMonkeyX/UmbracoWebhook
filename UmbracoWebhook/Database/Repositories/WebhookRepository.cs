using Umbraco.Cms.Infrastructure.Scoping;
using UmbracoWebhook.Database.Repositories.Interfaces;

namespace UmbracoWebhook.Database.Repositories;

public class WebhookRepository : IWebhookRepository
{
    private readonly IScopeProvider _scopeProvider;

    public WebhookRepository(IScopeProvider scopeProvider)
    {
        _scopeProvider = scopeProvider;
    }

    public void Delete(Webhook webhook)
    {
        using (var scope = _scopeProvider.CreateScope())
        {
            scope.Database.Delete(webhook);
            scope.Complete();
        }
    }

    public Webhook? Save(Webhook webhook)
    {
        using (var scope = _scopeProvider.CreateScope())
        {
            if (Exists(webhook.Id))
                return Update(webhook);

            var result = scope.Database.Insert(webhook) as Webhook;
            scope.Complete();
            return result;
        }
    }

    public Webhook? Get(int id)
    {
        using (var scope = _scopeProvider.CreateScope())
        {
            var result = scope.Database
                .Fetch<Webhook>()
                .FirstOrDefault(x => x.Id == id);
            scope.Complete();
            return result;
        }
    }

    public IEnumerable<Webhook> GetAll()
    {
        using (var scope = _scopeProvider.CreateScope())
        {
            var result = scope.Database.Fetch<Webhook>();
            scope.Complete();
            return result;
        }
    }

    public bool Exists(int id)
    {
        var webhook = Get(id);
        return webhook != null;
    }

    private Webhook Update(Webhook webhook)
    {
        if (Exists(webhook.Id) is false)
            return Save(webhook);

        using (var scope = _scopeProvider.CreateScope())
        {
            var id = scope.Database.Update(webhook);
            scope.Complete();
            return Get(id);
        }
    }
}