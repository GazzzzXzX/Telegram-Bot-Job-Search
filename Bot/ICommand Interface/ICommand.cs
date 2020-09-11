using System;
using Microsoft.VisualBasic.FileIO;
using Telegram.Bot;
using Telegram.Bot.Types;
using WorkShop.DataBase;
using User = WorkShop.DataBase.User;

namespace WorkShop.Bot.ICommand_Interface
{
	interface ICommand
	{
		/// <summary>
		/// Имя команды.
		/// </summary>
		public String Name { get; set; }

		/// <summary>
		/// Выполнение команды.
		/// </summary>
		/// <param name="botClient">Объект TelegramBotClient</param>
		/// <param name="message">Объект Message или CallbackQuery</param>
		public void Execute(TelegramBotClient botClient, Object message);

		/// <summary>
		/// Обработчик по имени команды, возвращает True если команды совподают.
		/// Возвращает False если команды не совподают.
		/// </summary>
		/// <param name="CommandName">Имя команды которую надо найти</param>
		/// <returns>Возвращает True или False</returns>
		public System.Boolean Equals(System.String CommandName)
		{
			if (CommandName != null)
			{
				if (Name == CommandName.Split(" ")[0])
				{
					System.String oldName = Name;
					Name = CommandName;
					return oldName.Equals(CommandName.Split(" ")[0]);
				}
			}
			return Name.Equals(CommandName);
		}
	}
}
