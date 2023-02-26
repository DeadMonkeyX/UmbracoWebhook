namespace UmbracoWebhook.Models;

public class PublishedWebhookData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public DateTime? PublishedDateTime { get; set; }
    public string ContentTypeAlias { get; set; }
}