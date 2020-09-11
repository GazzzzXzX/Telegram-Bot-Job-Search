using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WorkShop.DataBase;
using User = WorkShop.DataBase.User;

namespace WorkShop.Bot
{
	internal static class ExtentionsClient
	{
		public static async void DeleteMessage(this TelegramBotClient botClient, System.Int64 ChatOrFromId, System.Int32 messageId, System.String error)
		{
			try
			{
				await botClient.DeleteMessageAsync(ChatOrFromId, messageId);
			}
			catch (Exception ex)
			{
				System.Console.WriteLine("\n---------------\n" + "Сообщение не удалено! Код Строки: " + error + "\n" + ex + "\n---------------\n");
			}
		}

		public static async void SendText(this TelegramBotClient botClient, System.Int64 ChatOrFromId, System.String text, User user = null, IReplyMarkup replyMarkup = null, System.Boolean IsMarkdown = false)
		{
			try
			{
				Database db = Singleton.GetInstance().Context;
				Message mes;
				if (replyMarkup == null)
				{
					if (IsMarkdown)
					{
						mes = await botClient.SendTextMessageAsync(ChatOrFromId, text, Telegram.Bot.Types.Enums.ParseMode.Markdown);
					}
					else
					{

						mes = await botClient.SendTextMessageAsync(ChatOrFromId, text, Telegram.Bot.Types.Enums.ParseMode.Html);
					}
				}
				else
				{
					if (IsMarkdown)
					{
						mes = await botClient.SendTextMessageAsync(ChatOrFromId, text, Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: replyMarkup);
					}
					else
					{
						mes = await botClient.SendTextMessageAsync(ChatOrFromId, text, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: replyMarkup);
					}
				}
				if (user != null)
				{
					user.MessageId = mes.MessageId;
					db.UpsertRecord("Users", user.Id, user);
				}

			}
			catch(Exception ex)
			{
				System.Console.WriteLine("\n---------------\n" + "Сообщение не удалено! Код Строки: " + ex + "\n---------------\n");
			}
		}

		public static async void EditMessage(this TelegramBotClient botClient, System.Int64 ChatOrFromId, System.Int32 messageId, System.String text, System.String error, User user = null, InlineKeyboardMarkup replyMarkup = null, System.Boolean IsMarkdown = false)
		{
			Database db = Singleton.GetInstance().Context;
			try
			{
				Message mes;
				if (replyMarkup == null)
				{
					if (IsMarkdown)
					{
						mes = await botClient.EditMessageTextAsync(ChatOrFromId, messageId, text, Telegram.Bot.Types.Enums.ParseMode.Markdown);
					}
					else
					{
						mes = await botClient.EditMessageTextAsync(ChatOrFromId, messageId, text, Telegram.Bot.Types.Enums.ParseMode.Html);
					}
				}
				else
				{
					if (IsMarkdown)
					{
						mes = await botClient.EditMessageTextAsync(ChatOrFromId, messageId, text, Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: replyMarkup);
					}
					else
					{
						mes = await botClient.EditMessageTextAsync(ChatOrFromId, messageId, text, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: replyMarkup);
					}
				}
				if (user != null)
				{
					user.MessageId = mes.MessageId;
					db.UpsertRecord("Users", user.Id, user);
				}
			}
			catch (System.Exception ex)
			{
				System.Console.WriteLine("\n-------------\nНеудалось изменить текст! Код строки: " + error + "\n" + ex + "\n------------ -\n");
			}
		}
	}
}
