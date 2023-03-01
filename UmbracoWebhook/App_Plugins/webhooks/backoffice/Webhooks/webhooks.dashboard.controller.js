angular.module('umbraco').controller('webhookDashboard', function ($scope, $http, editorService) {

    const vm = this;

    vm.webhookList = [];

    vm.page = {
        title: "Webhooks",
        loading: true
    };

    vm.DeleteWebhook = function (webhook) {
        const confirmation = confirm(`⚠️ Are you sure you would like to delete: ${webhook.url} ⚠️?`)
        if (confirmation) {
            $http.delete(`/umbraco/backoffice/webhooks/${webhook.id}`).then((response) => {
                if (response.status == 204) {
                    vm.reloadData();
                }
            });
        }
    }

    vm.openWebhookCreateOverlay = function () {
        const options = {
            view: Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/Webhooks/backoffice/Webhooks/overlays/createOverlay.html',
            title: 'Create new webhook',
            submitButtonLabel: 'Save',
            closeButtonLabel: 'Close',
            size: 'medium',
            submit: async function (modal) {
                $http.post('/umbraco/backoffice/webhooks', modal.webhook).then((response) => {
                    vm.reloadData();
                    editorService.close();
                })
            },

            close: function () {
                editorService.close();
            },
        };
        editorService.open(options);
    }

    vm.reloadData = function () {
        $http.get("/umbraco/backoffice/webhooks").then((response) => {
            if (response.status === 200) {
                vm.webhookList = response.data;
            }
        });

    }

    vm.init = function () {
        vm.reloadData();
        vm.page.loading = false;

    }

    vm.init();

});
