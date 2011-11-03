namespace Accountability.Home
{
    using System.Linq;
    using HtmlTags;
    using Metrics;

    public class SearchResultsView
    {
        private readonly SearchFilters _Filters;

        public SearchResultsView(SearchFilters filters)
        {
            _Filters = filters;
        }

        public HtmlTag GetView()
        {
            var table = Tags.Table;
            AddItems(table);
            return table
                .Id("Results");
        }

        private void AddItems(TableTag table)
        {
            var results = _Filters.GetResults();
            results.ForEach(i => AddItem(table, i));
            if (!results.Any())
            {
                AddNoResultsFound(table);
            }
        }

        private static void AddNoResultsFound(TableTag table)
        {
            var row = table.AddBodyRow();
            row.Cell("No matches found");
        }

        private void AddItem(TableTag table, AccountabilityEvent item)
        {
            var row = table.AddBodyRow();
            row.Attr("key",
                     JsonUtil.ToJson(
                         new
                             {
                                 item.GetType().Name,
                                 Target = item.TargetId.ToString(),
                                 Metric = item.MetricId.ToString(),
                                 Source = item.SourceId.ToString()
                             }));
            row.Cell(item.GetSummary());
        }
    }
}