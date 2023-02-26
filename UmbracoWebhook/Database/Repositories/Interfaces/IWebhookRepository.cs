namespace UmbracoWebhook.Database.Repositories.Interfaces;

public interface IWebhookRepository
{
    /// <summary>
    ///     Deletes webhook from the database
    /// </summary>
    /// <param name="webhook"></param>
    void Delete(Webhook webhook);

    /// <summary>
    ///     Determines if a webhook exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool Exists(int id);

    /// <summary>
    ///     Creates or updates webhook
    /// </summary>
    /// <param name="webhook"></param>
    /// <returns>Webhook</returns>
    Webhook Save(Webhook webhook);


    /// <summary>
    ///     Gets webhook by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Webhook Get(int id);

    /// <summary>
    ///     Gets a list of all webhooks
    /// </summary>
    /// <returns></returns>
    IEnumerable<Webhook> GetAll();
}