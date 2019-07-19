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

    var api_url = "https://localhost:44302/api/movies/";

    $("#btnSearch").click(function () {

        $("#results").html('');

        var ddlFilters = document.getElementById("ddlFilters");
        var strFilter = ddlFilters.options[ddlFilters.selectedIndex].value;
        var txtSearch = document.getElementById("txtSearch").value;

        $.ajax({
            url: api_url + strFilter + "/" + txtSearch,
            contentType: "application/json",
            headers: { 'Access-Control-Allow-Origin': '*' },
            method: "GET",
            dataType: 'json',
            success: function (d) {
                $("#results").html(d);
                console.log(d);
            }
        })
    });

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
