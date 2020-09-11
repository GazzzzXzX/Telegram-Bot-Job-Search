using System;
using Telegram.Bot.Types;

namespace WorkShop.Bot.ICommand_Interface
{
	internal interface IMessage
	{
		public Boolean SetMessage(Object message, out Message _message)
		{
			_message = message as Message;
			return _message == null;
		}

		public Boolean SetMessage(Object message, out CallbackQuery callbackQuery)
		{
			callbackQuery = message as CallbackQuery;
			return callbackQuery == null;
		}
	}
}
