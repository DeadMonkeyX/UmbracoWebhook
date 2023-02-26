using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoWebhook.Database;
using UmbracoWebhook.Database.Repositories.Interfaces;

namespace UmbracoWebhook.Controllers.Api;

[Route("umbraco/backoffice/webhooks")]
[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
[ApiController]
public class WebhookApiController : UmbracoApiController
{
    private readonly IWebhookRepository _webhookRepository;

    public WebhookApiController(IWebhookRepository webhookRepository)
    {
        _webhookRepository = webhookRepository;
    }

    /// <summary>
    /// Gets a list of all the webhooks
    /// </summary>
    /// <returns>A list of Webhooks</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Webhook>> GetWebhooks()
    {
        return _webhookRepository.GetAll().ToList();
    }

    /// <summary>
    /// Gets one specific webhook  
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Webhook</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Webhook> GetWebhook(int id)
    {
        return _webhookRepository.Get(id);
    }

    /// <summary>
    /// Creates a new webhook
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /umbraco/backoffice/webhooks
    ///     {
    ///         "url": "webhook.site",
    ///         "contentTypeAlias": ""
    ///     }
    ///
    /// </remarks>
    /// <param name="webhook"></param>
    /// <returns>A newly created webhook</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<Webhook> CreateWebhook(Webhook webhook)
    {
        if (webhook is null) return UnprocessableEntity();
        _webhookRepository.Save(webhook);
        return CreatedAtAction(nameof(GetWebhook), new {id = webhook.Id}, webhook);
    }

    /// <summary>
    /// Deletes a webhook based on id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteWebhook(int id)
    {
        var webhook = _webhookRepository.Get(id);
        if (webhook == null) return NotFound();
        _webhookRepository.Delete(webhook);
        return NoContent();
    }
}