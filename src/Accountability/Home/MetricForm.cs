namespace Accountability.Home
{
    using System;
    using HtmlTags;
    using HtmlTags.Extensions;
    using Infrastructure;

    public class MetricForm
    {
        public HtmlTag GetView()
        {
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
            row.Cell().Nest(Tags.Span.DataBind("text: Target"));

            row = table.AddBodyRow();
            row.Cell("Source");
            row.Cell().Nest(Tags.Span.DataBind("text: Source"));

            row = table.AddBodyRow();
            row.Cell("Metric");
            row.Cell().Nest(Tags.Span.DataBind("text: Metric"));

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