﻿namespace Accountability.Admin
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;
	using BclExtensionMethods;
	using FluentMongo.Linq;
	using HtmlTags;
	using HtmlTags.Extensions;
	using HtmlTags.Extensions.Mvc;
	using HtmlTags.UI;
	using Mongos;

	public class AdminFilters
	{
		public enum AdminType
		{
			Metrics,
			Sources
		}

		public AdminType Type { get; set; }
		public string Search { get; set; }

		public HtmlTag FilterForm(HtmlHelper<AdminFilters> html, UrlHelper urlHelper)
		{
			var form = urlHelper.Form<AdminController>
				(c => c.IndexData(null))
				.Id("FilterForm")
				.Nest(
					Tags.Table.AddHeaderRow(r =>
					                        	{
					                        		r.Cell("Type");
					                        		r.Cell("Search");
					                        	})
						.AddBodyRow(r =>
						            	{
						            		r.Cell().Child(html.FilterFor(x => x.Type));
						            		r.Cell().Child(html.FilterFor(x => x.Search));
						            		r.Cell().Child(ViewConventionExtensions.ResetButton());
						            	}),
					Tags.Br,
					Tags.SubmitButton.Value("Search"))
				.Nest(ViewConventionExtensions.ExportButtons());
			return form;
		}

		public IEnumerable<AdminItem> GetAdminItems()
		{
			if (Type == AdminType.Metrics)
			{
				var metrics = Mongo.Metrics
					.AsQueryable();
				if (Search.IsNotNullOrWhiteSpace())
				{
					metrics = metrics.Where(m => m.Description.Contains(Search)
					                             || m.WhyItMatters.Contains(Search));
				}
				return metrics
					.Select(m => new AdminItem(m));
			}
			if (Type == AdminType.Sources)
			{
				var sources = Mongo.Sources
					.AsQueryable();
				if (Search.IsNotNullOrWhiteSpace())
				{
					sources = sources.Where(m => m.Name.Contains(Search));
				}
				return sources
					.Select(s => new AdminItem(s));
			}
			throw new NotSupportedException("Invalid admin type: " + Type);
		}
	}
}