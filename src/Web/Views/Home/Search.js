var viewModel;

$(function () {

    var ViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        
        this.results = ko.dependentObservable(function () {
            var query = {
                target: this.Target() ? this.Target().Value : "",
                source: this.Source() ? this.Source().Value : "",
                metric: this.Metric() ? this.Metric().Value : ""
            };
            $.ajax({
                url: "Home/SearchData",
                dataType: "jsonp",
                data: query
            }).pipe(function(r) { return r; });
        }, this).extend({ async: true });
    };

    viewModel = new ViewModel(initialModel);
    ko.applyBindings(viewModel);
});