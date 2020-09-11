using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WorkShop.Bot;
using WorkShop.DataBase;

namespace WorkShop
{
	class Program
	{
		static void Main(String[] args)
		{
			MyBot bot = new MyBot("1037516914:AAFoqHvayMn__5n2GvT5MHfidRT96q1WjhU");
			bot._exitEvent.WaitOne();
		}
	}
}
