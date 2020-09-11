using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using WorkShop.DataBase;

namespace WorkShop.Bot.Chain_Of_Responsibility
{
	internal interface IHandler
	{
		IHandler SetNext(IHandler handler);

		System.Object Handle(System.Int32 request, TelegramBotClient botClient, System.Object message);
	}

	internal abstract class AbstractHandler : IHandler
	{
		private IHandler _nextHandler;

		public IHandler SetNext(IHandler handler)
		{
			_nextHandler = handler;

			// Возврат обработчика отсюда позволит связать обработчики простым
			// способом, вот так:
			// monkey.SetNext(squirrel).SetNext(dog);
			return handler;
		}

		public virtual System.Object Handle(System.Int32 request, TelegramBotClient botClient, System.Object message)
		{
			if (_nextHandler != null)
			{
				return _nextHandler.Handle(request, botClient, message);
			}
			else
			{
				return null;
			}
		}
	}

	internal interface IHandlerAnaliz
	{
		IHandlerAnaliz SetNext(IHandlerAnaliz handler);

		System.Object Handle(TelegramBotClient botClient, System.Object message, User user);
	}

	internal abstract class AbstractHandlerAnaliz : IHandlerAnaliz
	{
		private IHandlerAnaliz _nextHandler;

		public virtual System.Object Handle(TelegramBotClient botClient, System.Object message, User user)
		{
			if (_nextHandler != null)
			{
				return _nextHandler.Handle(botClient, message, user);
			}
			else
			{
				return null;
			}
		}

		public IHandlerAnaliz SetNext(IHandlerAnaliz handler)
		{
			_nextHandler = handler;

			// Возврат обработчика отсюда позволит связать обработчики простым
			// способом, вот так:
			// monkey.SetNext(squirrel).SetNext(dog);
			return handler;
		}
	}
}
