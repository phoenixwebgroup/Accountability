namespace Accountability.Admin
{
	using BclExtensionMethods;
	using HtmlTags;

	public class AdminListView
	{
		private readonly AdminFilters _Filters;

		public AdminListView(AdminFilters filters)
		{
			_Filters = filters;
		}

		public HtmlTag GetView()
		{
			var table = Tags.Table;
			AddHeader(table);
			AddItems(table);
			return table;
		}

		private void AddItems(TableTag table)
		{
			_Filters.GetAdminItems().ForEach(i => AddItem(table, i));
		}

		private void AddItem(TableTag table, AdminItem item)
		{
			var row = table.AddBodyRow();
			row.Attr("key", item.AdminType + "-" + item.Id);
			row.Cell(item.AdminType.ToString());
			row.Cell(item.Summary);
		}

		private static void AddHeader(TableTag table)
		{
			var header = table.AddHeaderRow();
			header.Cell("Type");
			header.Cell("Description");
		}
	}
}