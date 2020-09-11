using System;
using Telegram.Bot.Types;
using WorkShop.DataBase;
using User = WorkShop.DataBase.User;

namespace WorkShop.Bot.ICommand_Interface
{
	internal interface IUserDataBase
	{
		public Boolean SetDataBase(out Database db)
		{
			db = Singleton.GetInstance().Context;
			return db == null;
		}

		public Boolean SetUser(Message _message, out User user, Database db)
		{
			user = db.LoadRecordById<User>("Users", _message.From.Id);

			return user == null;
		}

		public Boolean SetUser(CallbackQuery _message, out User user, Database db)
		{
			user = db.LoadRecordById<User>("Users", _message.From.Id);

			return user == null;
		}

		public Boolean SetUser(Int32 id, out User user, Database db)
		{
			user = db.LoadRecordById<User>("Users", id);

			return user == null;
		}
	}
}
