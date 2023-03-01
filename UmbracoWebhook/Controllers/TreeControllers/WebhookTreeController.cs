using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;
using Umbraco.Cms.Web.Common.Attributes;

namespace UmbracoWebhook.Controllers;

[PluginController("Webhooks")]
[Tree("settings", "Webhooks", TreeTitle = "Webhooks", TreeGroup = "webhooks", SortOrder = 5, IsSingleNodeTree = true)]
public class WebhookTreeController : TreeController
{
    public WebhookTreeController(ILocalizedTextService localizedTextService,
        UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection,
        IEventAggregator eventAggregator) : base(
        localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
    {
    }


    protected override ActionResult<TreeNodeCollection> GetTreeNodes(string id, FormCollection queryStrings)
    {
        return new TreeNodeCollection();
    }

    protected override ActionResult<MenuItemCollection> GetMenuForNode(string id, FormCollection queryStrings)
    {
        return null;
    }

    protected override ActionResult<TreeNode?> CreateRootNode(FormCollection queryStrings)
    {
        var rootResult = base.CreateRootNode(queryStrings);
        if (rootResult.Result is not null) return rootResult;

        var root = rootResult.Value;
        root.RoutePath = string.Format($"{Constants.Applications.Settings}/Webhooks/dashboard");
        root.Icon = "icon-hearts";
        root.HasChildren = false;
        root.MenuUrl = null;
        return root;
    }
}