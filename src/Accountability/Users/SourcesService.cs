namespace Accountability.Users
{
	using System.Collections.Generic;
	using System.Linq;
	using MongoDB.Bson;
	using Mongos;

	public class SourcesService
	{
		public List<Source> FindAll()
		{
			return Enumerable
				.Union<Source>(
					Mongo.Users.FindAll(),
					Mongo.Projects.FindAll())
				.ToList();
		}

		public Source Load(ObjectId sourceId)
		{
			return (Source) Mongo.Users.FindOneById(sourceId)
			       ?? Mongo.Projects.FindOneById(sourceId);
		}
	}
}