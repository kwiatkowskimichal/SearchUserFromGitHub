(function () {
    //register application
    var app = angular.module('GIT_SHOW_REPO', []);
})();

angular.module('GIT_SHOW_REPO').controller('gridController', function ($scope, DataService, $http) {

    //default variable
    $scope.data = null;
    $scope.totalCount = 0;
    $scope.name = "";
    $scope.location = "";
    $scope.avatar = "";
    $scope.loading = true;

    //set data and analize
    $scope.processData = function (db) {
        var contains = "";

        if (db.data == undefined) contains = db;
        else contains = db.data;

        if (contains.repository == null)
        {
            $scope.loading = false;
            $scope.data = 0;

            return;
        } 

        $scope.totalCount = contains.repository.length;

        //insert no      
        for (var j = 0; j < contains.repository.length; j++) {
            if (typeof contains.repository[j] != "undefined")
                contains.repository[j].count = j + 1;
        }

        $scope.data = contains.repository;
        $scope.name = contains.userDetails.name;
        $scope.location = contains.userDetails.location;
        $scope.avatar = contains.userDetails.avatarUrl;
        $scope.loading = false;
    }

    //parse and get user
    $scope.searchAnotherUser = function () {
        var name = $("#search_user");

        if (name.val().length == 0) {
            alert("Please, type some letters");
            name.focus().css("background-color", "pink");

            return;
        }

        $scope.loading = true;
        name.css("background-color", "white");

        $scope.GetUser(name.val());
    }

    //get specific user by name
    $scope.GetUser = function (name) {
        $http.get('Home/GetUserFromGitAsync', { params: { "name": name } }).success(function (data) {            
            $scope.processData(data);
        }).error(function (data) {
            alert("Error ocurred");
        });
    }

    //get data
    DataService.GeList().then(function (data) {
        $scope.processData(data);
    });

}).factory('DataService', function ($http) {
    var fac = {};
    fac.GeList = function () {
        //send default data  
        return $http.get('Home/GetUserFromGitAsync?name=robconery');
    }

    return fac;
});