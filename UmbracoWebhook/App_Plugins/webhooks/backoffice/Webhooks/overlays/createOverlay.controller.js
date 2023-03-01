angular.module('umbraco').controller('createOverlayController', function ($scope) {
    const vm = this;

    vm.model = {
        webhook: {
            Url: "",
            ContentTypeAlias: ""
        }
    };

    vm.save = function () {
        $scope.model.submit(vm.model);
    }

    vm.close = function () {
        if ($scope.model.close) {
            $scope.model.close();
        }
    };

});