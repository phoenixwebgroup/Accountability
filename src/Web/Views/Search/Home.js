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
	this.refresh = function () {
		$.ajax({
			url: 'Search/SearchFor',
			data: { text: me.search },
			success: function (data) {
				var update = { results: data };
				ko.mapping.updateFromJS(me, update);
			}
			//		dataType: dataType
		});
	};
	this.collapseAll = function () {
		Enumerable.From(this.results())
			.ForEach(function (r) { r.collapse(); });
	} .bind(this);
	if (data.search != 'Admin') {
		Enumerable.Range(1, 10)
			.Select(function (i) { return new Result({ title: 'result ' + i, body: 'Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integerut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sitamet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo utodio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.' }); })
			.ForEach(function (r) { me.results.push(r); });
	}
	else {
		this.refresh();
	}
};
var Result = function (data) {
	ko.mapping.fromJS(data, {}, this);
	this.expanded = ko.observable(false);
	this.collapse = function() {
		this.expanded(false);
	};
	this.toggleExpanded = function () {
		this.expanded(!this.expanded());
	} .bind(this);
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