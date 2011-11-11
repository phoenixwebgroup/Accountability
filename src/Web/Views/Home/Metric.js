var Metric = function (data, query) {
	var self = this;
	var parameters = ko.toJS(query);
	ko.mapping.fromJS(data, {}, this);

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