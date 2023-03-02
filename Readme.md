# UmbracoWebhook

I've have created a clean umbraco 11 with the Umbraco starterkit:

```bash
dotnet new sln -n UmbracoWebhook

dotnet new Umbraco -n UmbracoWebhook --friendly-name "Mads Schou" --email "mms@test.test" --password "mms@test.test" --development-database-type SQLite

dotnet sln add "UmbracoWebhook"

dotnet add "UmbracoWebhook" package Umbraco.TheStarterKit
```

## Implementation and thoughts

I have added functionality for creating webhooks that fires when a Page is published. Webhooks can be configured from the Backoffice settings section and it is possible to limit the webhook to only fire when a specific content type is published.

I have chosen to implement the calling of webhooks as an async task because my understating is that this solution is not big enough for running with a message queue and a job processor.

If was going with the message queue approach it could save a lot of time to depend on Hangfire because of the build in functionality for this type of operation.

## Json payload

I have chosen to interpret the `making the json payload sent in the webhook as clean as possible.` to mean that the payload should only contain information that we care about and enough information to query the site for more information.

Example of payload

```json
[
  {
    "id": 1103,
    "name": "Products",
    "url": "/products/",
    "publishedDateTime": "2023-03-01T16:02:08.9788517+01:00",
    "contentTypeAlias": "products"
  }
]
```
