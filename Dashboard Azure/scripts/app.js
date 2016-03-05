(function () {
    var app = angular.module("LinkPage", []);

    app.controller("PageController", function ($scope, $http) {
        var _controller = this;

        $scope.load = function () {
            $scope.generateColumns(function () {
                $(".loader").fadeOut(500, function () {
                    $(".linkContainer").fadeIn(500);
                });
            });
        };

        $scope.generateColumns = function () {
            $scope.generateColumns(function () { });
        };

        $scope.generateColumns = function (callback) {
            $http.post('Default.aspx/GetAllSites', { data: {} }).then(function (data) {
                if (callback) {
                    callback();
                }
                _controller.columns = ConstructColumns(eval(data.data.d));

            }, function (err) {
                console.log(err);
            });
        };

        $scope.addSite = function () {
            var popup = $(".siteOverlay");

            var data = {
                'Name': $(popup).find("#SiteName").val(),
                'Url': $(popup).find("#SiteUrl").val(),
                'BgColour': $(popup).find("#SiteBgColour").val(),
                'Colour': $(popup).find("#SiteColour").val()
            }

            $http.post("Default.aspx/InsertSite", { data: data, columnName: $(popup).find("#ColumnName").val() }).then(function (data) {
                $scope.generateColumns(function () {
                    overlay().close();
                    overlay().clear();
                });
            });
        };

        $scope.addColumn = function () {
            var popup = $(".columnOverlay");

            $http.post("Default.aspx/InsertColumn", { columnName: $(popup).find("#ColumnName").val() }).then(function (data) {
                $scope.generateColumns(function () {
                    overlay().close();
                    overlay().clear();
                });
            });
        };

        $scope.deleteSite = function (name) {
            $http.post("Default.aspx/DeleteSite", { siteName: name }).then(function (data) {
                $scope.generateColumns();
            });
        };

        $scope.deleteColumn = function (name) {
            $http.post("Default.aspx/DeleteColumn", { columnName: name }).then(function (data) {
                $scope.generateColumns();
            });
        };

    });

    function ConstructColumns(data) {
        var columns = [];
        $(data).each(function () {
            var isInColumns = false;
            var _data = this;
            $(columns).each(function () {
                if (this.Name === _data.Name) {
                    isInColumns = true;

                    this.Sites.push(_data.Sites);
                }
            });

            if (!isInColumns) {
                var temp = {
                    Name: _data.Name,
                    Sites: [_data.Sites]
                };
                columns.push(temp);
            }
        });

        return columns;
    }
})();

