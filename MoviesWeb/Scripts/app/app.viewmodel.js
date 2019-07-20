function AppViewModel(dataModel) {
    // Private state
    var self = this;

    // Private operations
    function cleanUpLocation() {
        window.location.hash = "";

        if (typeof history.pushState !== "undefined") {
            history.pushState("", document.title, location.pathname);
        }
    }

    var api_movies_url = "http://localhost:52630/api/movies/";
    var api_user_url = "http://localhost:52630/api/user/";

    $("#txtSearch").keypress(function (e) {
        if (e.keyCode == 13) {
            searchMovies();
            return false;
        }
    });

    $("#btnSearch").click(function () {
        searchMovies();        
    });

    function searchMovies() {
        $("#results").html('');

        var ddlFilters = document.getElementById("ddlFilters");
        var strFilter = ddlFilters.options[ddlFilters.selectedIndex].value;
        var txtSearch = document.getElementById("txtSearch").value;

        $.ajax({
            url: api_movies_url + strFilter + "/" + txtSearch,
            contentType: "application/json",
            headers: { 'Access-Control-Allow-Origin': '*' },
            method: "GET",
            dataType: 'json',
            success: function (d) {
                var data = JSON.stringify(d, null, "\t");
                $("#results").text(data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $("#results").text(errorThrown);
            }
        });
    }

    getTop5Movies();

    function getTop5Movies() {
        $("#top5movies").text('');

        $.ajax({
            url: api_movies_url + "top/5",
            contentType: "application/json",
            headers: { 'Access-Control-Allow-Origin': '*' },
            method: "GET",
            dataType: 'json',
            success: function (d) {
                var data = JSON.stringify(d, null, "\t");
                $("#top5movies").text(data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $("#top5movies").text(errorThrown);
            }
        })
    }

    $("#txtTopMovieUserSearch").keypress(function (e) {
        if (e.keyCode == 13) {
            getTop5MoviesByUser();
            return false;
        }
    });

    $("#btnTopMovieUserSearch").click(function () {
        getTop5MoviesByUser();
    });

    function getTop5MoviesByUser() {
        $("#top5moviesByUser").text('');

        var txtTopMovieUserSearch = document.getElementById("txtTopMovieUserSearch").value;

        $.ajax({
            url: api_movies_url + "top/5/user/" + txtTopMovieUserSearch,
            contentType: "application/json",
            headers: { 'Access-Control-Allow-Origin': '*' },
            method: "GET",
            dataType: 'json',
            success: function (d) {
                var data = JSON.stringify(d, null, "\t");
                $("#top5moviesByUser").text(data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $("#top5moviesByUser").text(errorThrown);
            }
        })
    }


    $("#btnUserRatingSave").click(function () {
        submitUserRating();
    });

    function submitUserRating() {
        $("#userRatingSummary").html('');

        var txtFullName = document.getElementById("txtUser").value;
        var txtMovieId = document.getElementById("txtMovieId").value;
        var txtRating = document.getElementById("txtRating").value;

        $.ajax({
            url: api_user_url + txtFullName + "/movie/" + txtMovieId + "/rating/" + txtRating,
            contentType: "application/json",
            headers: { 'Access-Control-Allow-Origin': '*' },
            method: "PUT",
            dataType: 'json',
            success: function (d) {
                var data = JSON.stringify(d, null, "\t");
                $("#userRatingSummary").text(data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $("#userRatingSummary").text(errorThrown);
            }
        });
    }

    // Data
    self.Views = {
        Loading: {} // Other views are added dynamically by app.addViewModel(...).
    };
    self.dataModel = dataModel;

    // UI state
    self.view = ko.observable(self.Views.Loading);

    self.loading = ko.computed(function () {
        return self.view() === self.Views.Loading;
    });

    // UI operations

    // Other navigateToX functions are added dynamically by app.addViewModel(...).

    // Other operations
    self.addViewModel = function (options) {
        var viewItem = new options.factory(self, dataModel),
            navigator;

        // Add view to AppViewModel.Views enum (for example, app.Views.Home).
        self.Views[options.name] = viewItem;

        // Add binding member to AppViewModel (for example, app.home);
        self[options.bindingMemberName] = ko.computed(function () {
            if (!dataModel.getAccessToken()) {
                // The following code looks for a fragment in the URL to get the access token which will be
                // used to call the protected Web API resource
                var fragment = common.getFragment();

                if (fragment.access_token) {
                    // returning with access token, restore old hash, or at least hide token
                    window.location.hash = fragment.state || '';
                    dataModel.setAccessToken(fragment.access_token);
                } else {
                    // no token - so bounce to Authorize endpoint in AccountController to sign in or register
                    window.location = "/Account/Authorize?client_id=web&response_type=token&state=" + encodeURIComponent(window.location.hash);
                }
            }

            return self.Views[options.name];
        });

        if (typeof options.navigatorFactory !== "undefined") {
            navigator = options.navigatorFactory(self, dataModel);
        } else {
            navigator = function () {
                window.location.hash = options.bindingMemberName;
            };
        }

        // Add navigation member to AppViewModel (for example, app.NavigateToHome());
        self["navigateTo" + options.name] = navigator;
    };

    self.initialize = function () {
        Sammy().run();
    };
}

var app = new AppViewModel(new AppDataModel());
