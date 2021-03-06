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
			Metric,
			User,
			Project
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
			if (Type == AdminType.Metric)
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
			if (Type == AdminType.User)
			{
				var users = Mongo.Users
					.AsQueryable();
				if (Search.IsNotNullOrWhiteSpace())
				{
					users = users.Where(m => m.Name.Contains(Search));
				}
				return users
					.Select(s => new AdminItem(s));
			}
			if (Type == AdminType.Project)
			{
				var projects = Mongo.Projects
					.AsQueryable();
				if (Search.IsNotNullOrWhiteSpace())
				{
					projects = projects.Where(m => m.Name.Contains(Search));
				}
				return projects
					.Select(s => new AdminItem(s));
			}
			throw new NotSupportedException("Invalid admin type: " + Type);
		}
	}
}