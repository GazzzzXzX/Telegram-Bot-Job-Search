using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WorkShop.DataBase
{
	class AdminPost
	{
		[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
		public String Id { get; set; }

		public String PostId { get; set; }

		public Int32 userId { get; set; }

		public Int32 AdminId { get; set; }

		public String PostText { get; set; }

		public Boolean IsCheck { get; set; }
	}
}
