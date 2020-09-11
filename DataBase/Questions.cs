using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WorkShop.DataBase
{
	class Questions
	{
		[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
		public String Id { get; set; }

		public User Admin { get; set; }

		public User user { get; set; }

		public String Question { get; set; }

		public String Answer { get; set; }
	}
}
