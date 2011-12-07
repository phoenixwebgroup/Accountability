var viewModel;

$(function () {

	var ViewModel = function (data) {
		var root = this;
		ko.mapping.fromJS(data, {}, this);

		this.Type.subscribe(function () {
			root.UserId("");
		});

		this.UserFilterLabel = ko.dependentObservable(function () {
			if (root.Type() == "1") {	 // for me
				return "Source";
			}
			return "Target";
		});

		this.query = ko.dependentObservable(function () {
			return {
				Type: root.Type(),
				UserId: root.UserId(),
				MetricId: root.MetricId()
			};
		}, this);

		this.results = asyncDependentObservable(function () {
			var query = root.query();
			return $.ajax({
				url: "Home/SearchData",
				dataType: "json",
				data: query
			}).pipe(function (r) { return r; });
		}, this);

		this.ShowAddButton = ko.dependentObservable(function () {
			var hasUser = root.UserId() != '';
			var hasMetric = root.MetricId() != '';
			return hasUser && hasMetric; // check truthiness
		}, this);

		this.ShowGiveFeedback = function () {
			root.GiveFeedback.IsVisible(true);
		};

		this.ShowAddActionItem = function () {
			root.AddActionItem.IsVisible(true);
		};

		this.ShowScheduleNextReview = function () {
			root.ScheduleNextReview.IsVisible(true);
		};

		this.GiveFeedback = new GiveFeedback(this);
		this.AddActionItem = new AddActionItem(this);
		this.ScheduleNextReview = new ScheduleNextReview(this);
	};

	var GiveFeedback = function (root) {
		var me = this;

		this.IsVisible = ko.observable(false);

		this.Model = ko.observable(getBlankFeedback());

		this.SaveFeedback = function () {
			var feedback = ko.mapping.toJS(me.Model());
			feedback.targetId = root.UserId();
			feedback.metricId = root.MetricId();
			$.ajax({
				type: "POST",
				url: "Home/GiveFeedback",
				data: feedback,
				success: function () {
					me.Model(getBlankFeedback());
					root.query.notifySubscribers(root.query());
				}
			});
		};

		this.Done = function () {
			me.IsVisible(false);
		};
	};

	var AddActionItem = function (root) {
		var me = this;

		this.IsVisible = ko.observable(false);

		this.Model = ko.observable(getBlankActionItem());

		this.SaveActionItem = function () {
			var feedback = ko.mapping.toJS(me.Model());
			feedback.targetId = root.UserId();
			feedback.metricId = root.MetricId();
			$.ajax({
				type: "POST",
				url: "Home/AddActionItem",
				data: feedback,
				success: function () {
					me.Model(getBlankActionItem());
					root.query.notifySubscribers(root.query());
				}
			});
		};

		this.Done = function () {
			me.IsVisible(false);
		};
	};

	var ScheduleNextReview = function(root) {
		var me = this;

		this.IsVisible = ko.observable(false);

		this.Model = ko.observable(getBlankNextReview());

		this.SaveNextReview = function() {
			var feedback = ko.mapping.toJS(me.Model());
			feedback.targetId = root.UserId();
			feedback.metricId = root.MetricId();
			$.ajax({
					type: "POST",
					url: "Home/ScheduleNextReview",
					data: feedback,
					success: function() {
						me.Model(getBlankNextReview());
						root.query.notifySubscribers(root.query());
					}
				});
		};

		this.Done = function() {
			me.IsVisible(false);
		};
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