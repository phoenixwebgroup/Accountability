namespace Accountability.Home
{
    using System;
    using System.Collections.Generic;
    using HtmlTags.UI;
    using HtmlTags.UI.Attributes;
    using Mongos;

    public class SearchFilters
    {
        public SearchFilters()
        {
            // todo Source = current user
        }

        [OptionsFrom("Users")]
        public string Target { get; set; }

        [OptionsFrom("Users"), WithBlankOption]
        public string Source { get; set; }

        [OptionsFrom("Metrics"), WithBlankOption]
        public string Metric { get; set; }

        public Options Users
        {
            get { return Mongo.Users.FindAll().ToOptions(x => x.Name, x => x.Name); }
        }

        public Options Metrics
        {
            get { return Mongo.Metrics.FindAll().ToOptions(x => x.Id, x => x.Name); }
        }

        public IEnumerable<Result> GetResults()
        {
           throw new NotImplementedException();
        }
    }

    public struct Result
    {
        // todo
    }
}