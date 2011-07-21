var HomeModel = function (data) {
	ko.mapping.fromJS(data, {}, this);
	this.toggleSearch = function (searchType) {
		var existingSearch = Enumerable.From(homeModel.searches())
			.Where(function (s) { return s.search() === searchType; })
			.FirstOrDefault();
		if (existingSearch == undefined) {
			var search = {
				search: searchType,
				results: []
			};
			homeModel.searches.push(new Search(search));
			return;
		}
		homeModel.searches.remove(existingSearch);
	};
};

var Search = function (data) {
	var mapping = {
		results: {
			create: function (options) {
				return new Result(options.data);
			}
		}
	};
	ko.mapping.fromJS(data, mapping, this);
	var me = this;
	Enumerable.Range(1, 10)
		.Select(function (i) { return new Result({ title: 'result ' + i }); })
		.ForEach(function (r) { me.results.push(r); });
};
var Result = function (data) {
	ko.mapping.fromJS(data, {}, this);
	
};

var homeModel;
var ResultTemplate = 'ResultTemplate';
$(function () {
	$('.resultSet').live('ready', function () {
		console.log(this);
		this.accordian();
	});
	var mapping = {
		create: function (options) {
			return new HomeModel(options.data);
		},
		searches: {
			create: function (options) {
				return new Search(options.data);
			}
		}
	};
	var data = {
		searches: []
	};
	homeModel = ko.mapping.fromJS(data, mapping, homeModel);
	ko.applyBindings(homeModel);

	homeModel.toggleSearch("Admin");
	homeModel.toggleSearch("Meta");
});