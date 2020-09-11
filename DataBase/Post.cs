using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;


namespace WorkShop.DataBase
{
	class Post
	{
		[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
		public String Id { get; set; }

		public Int32 userId { get; set; }

		public String PostText { get; set; }

		public Boolean IsPay { get; set; } = false;

		public Boolean IsCheck { get; set; } = false;

		public Boolean IsPublication { get; set; } = false;

		public Boolean IsWork { get; set; }
	}
}
