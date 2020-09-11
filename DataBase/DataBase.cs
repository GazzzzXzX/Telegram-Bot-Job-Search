using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WorkShop.DataBase
{
	public class Database
	{
		private IMongoDatabase db;

		public Database()
		{
			var client = new MongoClient();
			db = client.GetDatabase("Client");
		}

		public void InsertRecord<T>(String table, T record)
		{
			var collection = db.GetCollection<T>(table);
			collection.InsertOne(record);
		}

		public List<T> LoadRecords<T>(String table)
		{
			var collection = db.GetCollection<T>(table);

			return collection.Find(new BsonDocument()).ToList();
		}

		public T LoadRecordById<T>(String table, Int32 id)
		{
			var collection = db.GetCollection<T>(table);
			var filter = Builders<T>.Filter.Eq("Id", id);
			try
			{
				return collection.Find(filter).First();
			}
			catch
			{
				return default;
			}
		}

		public T LoadRecordById<T>(String table, String id)
		{
			var collection = db.GetCollection<T>(table);
			var filter = Builders<T>.Filter.Eq("Id", id);
			try
			{
				return collection.Find(filter).First();
			}
			catch
			{
				return default;
			}
		}

		public T LoadRecordByIdAdminPost<T>(String table, String id)
		{
			var collection = db.GetCollection<T>(table);
			var filter = Builders<T>.Filter.Eq("PostId", id);
			try
			{
				return collection.Find(filter).First();
			}
			catch
			{
				return default;
			}
		}

		public T LoadRecordByIdPost<T>(String table, Int32 id)
		{
			var collection = db.GetCollection<T>(table);
			var filter = new BsonDocument("userId", id);
			filter.Add("IsWork", true);
			try
			{
				return collection.Find(filter).First();
			}
			catch
			{
				return default;
			}
		}

		public T LoadRecordByIdPostPay<T>(String table, Int32 id)
		{
			var collection = db.GetCollection<T>(table);
			var filter = new BsonDocument("userId", id);
			filter.Add("IsWork", false);
			filter.Add("IsPay", false);
			filter.Add("IsCheck", true);
			filter.Add("IsPublication", false);
			try
			{
				return collection.Find(filter).First();
			}
			catch
			{
				return default;
			}
		}

		public void UpsertRecord<T>(String table, Int32 Id, T record)
		{
			var collection = db.GetCollection<T>(table);

			var result = collection.ReplaceOne(
				new BsonDocument("_id", Id),
				record,
				new UpdateOptions { IsUpsert = true });
		}

		public void UpsertRecord<T>(String table, String Id, T record)
		{
			var collection = db.GetCollection<T>(table);

			var result = collection.ReplaceOne(
				new BsonDocument("_id", Id),
				record,
				new UpdateOptions { IsUpsert = true });
		}

		public void DeleteRecord<T>(String table, Int32 Id)
		{
			var collection = db.GetCollection<T>(table);
			var filter = Builders<T>.Filter.Eq("Id", Id);

			collection.DeleteOne(filter);
		}

		public void DeleteRecord<T>(String table, String Id)
		{
			var collection = db.GetCollection<T>(table);
			var filter = Builders<T>.Filter.Eq("Id", Id);

			collection.DeleteOne(filter);
		}
	}
}
