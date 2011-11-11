namespace Accountability.Home
{
    using System;
    using Authentication;
    using HtmlTags;
    using HtmlTags.Extensions;
    using HtmlTags.UI;
    using Infrastructure;
    using Metrics;

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
            row.Cell("Target:");
            row.Cell().Nest(Tags.Span.DataBind("text: Target"));

            row = table.AddBodyRow();
            row.Cell("Source:");
            // todo Ability to view from all sources
            row.Cell().Nest(Tags.Span.DataBind("text: Source"));

            row = table.AddBodyRow();
            row.Cell("Metric:");
            row.Cell().Nest(Tags.Span.DataBind("text: Metric"));

            div.Nest(table);
        }

        private void AddReviewDates(HtmlTag div)
        {
            // TODO Add review dates template
        }

        private void AddFeedback(HtmlTag div)
        {
            var table = Tags.Table.Caption("Feedback");
            var headerRow = table.AddHeaderRow();
            headerRow.Cell("Date");
            headerRow.Cell("From");
            headerRow.Cell("Rating");
            headerRow.Cell("Notes");
            headerRow.Cell();

            var addRow = new TableRowTag();
            addRow.AddClass("top");
            addRow.Cell().Nest(Tags.Span.DataBind("text: Date"));
            addRow.Cell().Nest(Tags.Span.DataBind("text: Source"));
            addRow.Cell().Nest(new GiveFeedback().InputFor(x => x.Rating).DataBind("value: Rating"));
            addRow.Cell().Nest(new HtmlTag("textarea").DataBind("value: Notes"));
            addRow.Cell().Nest(
                Tags.Link.Href("#").Text("Save").DataBind("click: function() { $parent.saveFeedback(); }"));
            var addBody = new HtmlTag("tbody").DataBind("with: NewFeedback");
            addBody.Nest(addRow);

            var noteRow = new TableRowTag();
            noteRow.AddClass("note");
            noteRow.Cell().Nest(Tags.Span.DataBind("text: Date"));
            noteRow.Cell().Nest(Tags.Span.DataBind("text: Source"));
            noteRow.Cell().Nest(Tags.Span.DataBind("text: Rating"));
            noteRow.Cell().Nest(Tags.Span.DataBind("text: Notes"));
            noteRow.Cell();
            var notesBody = new HtmlTag("tbody").DataBind("foreach: Feedback");
            notesBody.Nest(noteRow);

            table.Nest(
                addBody,
                notesBody
                );

            table.AddClasses("report", "no-hover");
            div.Nest(table);
        }

        private void AddActionItems(HtmlTag div)
        {
            // TODO Add action items template
        }

        public string GetBlankFeedback()
        {
            var json = new
                           {
                               Source = UserPrincipal.Current.User.Name,
                               SourceId = UserPrincipal.Current.User.Id.ToString(),
                               Date = DateTime.Today,
                               Notes = "",
                               Rating = ""
                           };
            return string.Format("function getBlankFeedback(){{ return ko.mapping.fromJS({0}); }}",
                                 Serialize.Javascript(json));
        }
    }
}