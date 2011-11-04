var viewModel;

$(function () {

    var ViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);

        this.results = asyncDependentObservable(function() {
            var query = {
                target: this.Target() ? this.Target().Value : "",
                source: this.Source() ? this.Source().Value : "",
                metric: this.Metric() ? this.Metric().Value : ""
            };
            return $.ajax({
                    url: "Home/SearchData",
                    dataType: "json",
                    data: query
                }).pipe(function(r) { return r; });
        }, this);
    };

    viewModel = new ViewModel(initialModel);
    ko.applyBindings(viewModel);
});

// todo KO 1.3 is supposed to have something like this
function asyncDependentObservable(evaluator, owner) {
    var result = ko.observable(), currentDeferred;
    result.inProgress = ko.observable(false); // Track whether we're waiting for a result

    ko.dependentObservable(function () {
        // Abort any in-flight evaluation to ensure we only notify with the latest value
        if (currentDeferred) { currentDeferred.reject(); }

        var evaluatorResult = evaluator.call(owner);
        // Cope with both asynchronous and synchronous values
        if (evaluatorResult && (typeof evaluatorResult.done == "function")) { // Async
            result.inProgress(true);
            currentDeferred = $.Deferred().done(function (data) {
                result.inProgress(false);
                result(data);
            });
            evaluatorResult.done(currentDeferred.resolve);
        } else // Sync
            result(evaluatorResult);
    });

    return result;
}