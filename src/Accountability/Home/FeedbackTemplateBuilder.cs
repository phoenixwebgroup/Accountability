namespace Accountability.Home
{
    using System;
    using Authentication;
    using HtmlTags;
    using HtmlTags.Extensions;
    using HtmlTags.UI;
    using Infrastructure;
    using Metrics;
    using Microsoft.Practices.ServiceLocation;

    public static class FeedbackTemplateBuilder
    {
        private static UserPrincipal Principal
        {
            get { return ServiceLocator.Current.GetInstance<UserPrincipal>(); }
        }

        public static HtmlTag GetFeedbackTemplate()
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
            addRow.Cell().Nest(Tags.Link.Href("#").Text("Save").DataBind("click: function() { $parent.saveNote(); }"));
            var addBody = new HtmlTag("tbody").DataBind("with: Note");
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

        public static string BlankFeedbackFunction()
        {
            return string.Format("this.BlankFeedback = function(){{ return ko.mapping.fromJS({0}); }}",
                                 Serialize.Javascript(FeedbackJson));
        }

        public static object FeedbackJson
        {
            get
            {
                return new
                           {
                               SourceId = Principal.User.Id,
                               Source = Principal.User.Name,
                               Date = DateTime.Today,
                               Feedback = "",
                               Rating = ""
                           };
            }
        }
    }
}