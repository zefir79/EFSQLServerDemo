(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', 'apiService'/*, 'notificationService'*/];

    
    function indexCtrl($scope, apiService, notificationService) {
        //$scope.user = null;
        //$scope.latestAccount = null;
        //$scope.accounts = [];
        $scope.userAccount = null;
        $scope.loadData = loadData;
       
        function loadData() {
            apiService.get('/EFWebAppPrototype/api/data/useraccount', null,
                        userAccountLoadCompleted,
                        userAccountLoadFailed);
            //apiService.get('/EFWebAppPrototype/api/data/user', null,
            //            userLoadCompleted,
            //            userLoadFailed);

            //apiService.get('/EFWebAppPrototype/api/data/latestaccount', null,
            //    latestAccountLoadCompleted,
            //    latestAccountLoadFailed);

            //apiService.get('/EFWebAppPrototype/api/data/accounts', null,
            //   accountsLoadCompleted,
            //   accountsLoadFailed);
        }

        //User Account - flat object
        function userAccountLoadCompleted(result) {
            $scope.userAccount = result.data;
        }

        function userAccountLoadFailed(response) {
            //notificationService.displayError(response.data);
        }



        ////User
        //function userLoadCompleted(result) {
        //    $scope.user = result.data;
        //}
        
        //function userLoadFailed(response) {
        //    //notificationService.displayError(response.data);
        //}

        ////Latest account
        //function latestAccountLoadCompleted(result) {
        //    $scope.latestAccount = result.data;
        //}

        //function latestAccountLoadFailed(response) {
        //    //notificationService.displayError(response.data);
        //}

        ////Accounts
        //function accountsLoadCompleted(result) {
        //    $scope.accounts = result.data;
        //}

        //function accountsLoadFailed(response) {
        //    //notificationService.displayError(response.data);
        //}

        loadData();
    }

})(angular.module('home'));