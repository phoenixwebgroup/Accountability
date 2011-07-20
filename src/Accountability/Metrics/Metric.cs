namespace Accountability.Metrics
{
	using System.Collections.Generic;
	using MongoDB.Bson;

	public class Metric
	{
		public ObjectId Id { get; set; }

		/// <summary>
		/// 	Person, Project, Client, Company, Team etc
		/// </summary>
		public List<string> Subjects { get; set; }

		/// <summary>
		/// 	The name of the metric, to be reused across different subjects
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 	List of historical goals
		/// </summary>
		public List<Goal> Goals { get; set; }

		public Metric()
		{
			Id = ObjectId.GenerateNewId();
			Goals = new List<Goal>();
			Subjects = new List<string>();
			Samples = new List<Sample>();
			SamplingIntervals = new List<SamplingInterval>();
		}

		/// <summary>
		/// 	Intervals at which the metric should be sampled
		/// </summary>
		public List<SamplingInterval> SamplingIntervals { get; set; }

		public List<Sample> Samples { get; set; }
	}
}