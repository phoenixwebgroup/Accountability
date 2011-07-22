namespace Accountability.Search
{
	using System.Collections.Generic;
	using System.Linq;

	public class ResultSet
	{
		private readonly IEnumerable<SimpleResult> _Results;
		private int? _Count;

		public ResultSet(IEnumerable<SimpleResult> results)
		{
			_Results = results;
		}

		public void Limit(int count)
		{
			_Count = count;
		}

		public IEnumerable<SimpleResult> GetResults()
		{
			if (_Count.HasValue)
			{
				return _Results.Take(_Count.Value);
			}
			return _Results;
		}
	}
}