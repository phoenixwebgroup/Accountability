namespace Accountability.Home
{
    using System;
    using HtmlTags;
    using HtmlTags.Extensions;
    using Metrics;

    public class MetricForm
    {
        private readonly SearchFilters _Filters;
        private MetricView _Metric;

        public MetricForm(SearchFilters filters)
        {
            _Filters = filters;
        }

        public HtmlTag GetView()
        {
            var events = _Filters.Match();
            _Metric = new MetricView(events);
            var div = Tags.Div;
            AddCriteria(div);
            AddReviewDates(div);
            AddFeedback(div);
            AddActionItems(div);
            return div;
        }

        private void AddCriteria(HtmlTag div)
        {
            var table = Tags.Table;
            var row = table.AddBodyRow();
            row.Cell("Target");
            row.Cell(string.Join(", ", _Metric.Targets));

            row = table.AddBodyRow();
            row.Cell("Source");
            row.Cell(string.Join(", ", _Metric.Sources));

            row = table.AddBodyRow();
            row.Cell("Metric");
            row.Cell(_Metric.Metric);

            div.Nest(table);
        }

        private void AddReviewDates(HtmlTag div)
        {
            throw new NotImplementedException();
        }

        private void AddFeedback(HtmlTag div)
        {
            throw new NotImplementedException();
        }

        private void AddActionItems(HtmlTag div)
        {
            throw new NotImplementedException();
        }
    }
}