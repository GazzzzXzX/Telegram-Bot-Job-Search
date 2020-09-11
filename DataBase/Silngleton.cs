using System;
using System.Collections.Generic;
using System.Text;

namespace WorkShop.DataBase
{
	class Singleton
	{

		private Singleton()
		{
		}

		private static Singleton _instance;

		public static Singleton GetInstance()
		{
			if (_instance != null)
			{
				return _instance;
			}
		
			_instance = new Singleton
			{
				Context = new Database()
			};
			
			return _instance;
		}

		public Database Context { get; set; }
	}
}
