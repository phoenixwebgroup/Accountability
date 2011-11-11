namespace Accountability.Home
{
    using System;
    using Authentication;
    using HtmlTags;
    using HtmlTags.Extensions;
    using HtmlTags.UI;
    using Infrastructure;
    using Metrics;

    public class MetricPartialView
    {
        private readonly UserPrincipal _Principal;

        public MetricPartialView(UserPrincipal principal)
        {
            _Principal = principal;
        }

        public HtmlTag GetFeedbackTemplate()
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
            addRow.Cell().Nest(new HtmlTag("textarea").DataBind("text: Notes"));
            addRow.Cell().Nest(Tags.Link.Href("#").Text("Save").DataBind("click: function() { $root.saveFeedback($data); }"));
            var addBody = new HtmlTag("tbody").DataBind("with: Feedback");
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

            return table.AddClasses("report", "no-hover");
        }

        public string BlankFeedbackFunction()
        {
            return string.Format("this.BlankFeedback = function(){{ return ko.mapping.fromJS({0}); }}",
                                 Serialize.Javascript(FeedbackJson));
        }

        public object FeedbackJson
        {
            get
            {
                return new
                {
                    SourceId = _Principal.User.Id,
                    Source = _Principal.User.Name,
                    Date = DateTime.Today,
                    Feedback = "",
                    Rating = ""
                };
            }
        }
    }
}