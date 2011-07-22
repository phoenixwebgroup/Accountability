namespace Accountability.Search
{
	using HtmlTags;
	using HtmlTags.Extensions;

	public class HomeView
	{
		public HtmlTag Build()
		{
			return Tags.Div
				.Nest(GetSearchBar())
				.Nest(GetSearchesPane());
		}

		private HtmlTag GetSearchesPane()
		{
			var searches = Tags.Div.AddClass("SearchPane");
			var table = Tags.Table;
			table.AddClass("SearchPane");
			var row = table.AddBodyRow();
			row.Attr("data-bind", "template: { name: 'SearchPaneTemplate', foreach: homeModel.searches }");
			return searches
				.Nest(table)
				.Nest(GetSearchPaneTemplate())
				.Nest(GetResultTemplate())
				;
		}

		private HtmlTag GetSearchPaneTemplate()
		{
			var template = Tags.Script(string.Empty)
				.Attr("type", "text/html")
				.Id("SearchPaneTemplate");

			var cell = Tags.Cell.AddClass("SearchPane");

			var headerText = Tags.Span.Attr("data-bind", "text: search");
			var header = Tags.Div.Nest(headerText).AddClass("SearchTitle");
			cell.Nest(header);

			var results = Tags.Div
				.AddClass("resultSet")
				.Attr("data-bind", "template: { name: ResultTemplate, foreach: results }");
			cell.Nest(results);

			return template.Nest(cell);
		}

		private HtmlTag GetResultTemplate()
		{
			var template = Tags.Script(string.Empty)
				.Attr("type", "text/html")
				.Id("ResultTemplate");

			var result = Tags.Div.AddClass("Result");
			var title = Tags.Div.Nest(Tags.Span.Attr("data-bind", "text: title")).AddClass("ResultTitle");
			var body = Tags.Div.Nest(Tags.Span.Attr("data-bind", "text: body")).AddClass("ResultBody");

			result.Nest(title).Nest(body);
			return template
				.Nest(result);
		}

		private static HtmlTag GetSearchBar()
		{
			var searchBar = Tags.Div.AddClass("SearchBar");
			searchBar.Nest(GetSearch("Admin"));
			searchBar.Nest(GetSearch("Metrics"));
			searchBar.Nest(GetSearch("Favorites"));
			searchBar.Nest(GetSearch("Searches"));
			return searchBar;
		}

		private static HtmlTag GetSearch(string search)
		{
			return Tags.InputButton.Id(search).AddClass("toggleButton").Value(search).Attr("data-bind", "click: function(){ homeModel.toggleSearch('" + search + "');}");
		}
	}
}