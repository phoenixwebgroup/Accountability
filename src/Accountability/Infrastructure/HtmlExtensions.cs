namespace Accountability.Infrastructure
{
    using HtmlTags;

    public static class HtmlExtensions
    {
        public static HtmlTag DataBind(this HtmlTag tag, string appendValue)
        {
            var value = string.Empty;
            if (tag.HasAttr("data-bind"))
            {
                value = tag.Attr("data-bind") + ", ";
            }
            value += appendValue;
            tag.Attr("data-bind", value);
            return tag;
        } 
    }
}