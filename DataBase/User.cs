using System;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkShop.DataBase
{
	internal class User
	{
		[BsonId]
		public Int32 Id { get; set; }

		public String Username { get; set; }

		public String Language { get; set; }

		public Int32 Chain { get; set; }

		public Int32 MessageId { get; set; }

		public String questions { get; set; }

		public String PostId { get; set; }
	}
}
