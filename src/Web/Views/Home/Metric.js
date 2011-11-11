var Metric = function (data) {
	var self = this;
	ko.mapping.fromJS(data, {}, this);
	var parameters = {
		targetId: self.TargetId(),
		metricId: self.MetricId()
	};
	this.NewFeedback = ko.observable(getBlankFeedback());

	this.saveFeedback = function () {
		var feedback = ko.mapping.toJS(self.NewFeedback());
		feedback = $.extend(feedback, parameters);
		self.Feedback.splice(0, 0, feedback);
		$.ajax({
			type: "POST",
			url: "Home/GiveFeedback",
			data: feedback,
			success: function () {
				self.NewFeedback(getBlankFeedback());
			}
		});
	} .bind(this);
}