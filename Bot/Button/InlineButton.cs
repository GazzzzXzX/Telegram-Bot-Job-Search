using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;
using WorkShop.DataBase;

namespace WorkShop.Bot.Button
{
	static class InlineButton
	{
		public static InlineKeyboardMarkup Start()
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>
			{
				new List<InlineKeyboardButton>()
			};

			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "🇷🇺Russian",
				CallbackData = CommandName.Russian
			});
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "🏴󠁧󠁢󠁥󠁮󠁧󠁿English",
				CallbackData = CommandName.English
			});

			return new InlineKeyboardMarkup(list);
		}

		public static InlineKeyboardMarkup Menu(User user)
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>
			{
				new List<InlineKeyboardButton>()
			};
			if (user.Language == "RU")
			{
				list[list.Count - 1].Add(new InlineKeyboardButton()
				{
					Text = "🛠Найти исполнителя",
					CallbackData = CommandName.FindExecutor
				});
				list[list.Count - 1].Add(new InlineKeyboardButton()
				{
					Text = "📢Ищу работу",
					CallbackData = CommandName.FindWork
				});

				list.Add(new List<InlineKeyboardButton>());
				list[list.Count - 1].Add(new InlineKeyboardButton()
				{
					Text = "📞Связь с администратором",
					CallbackData = CommandName.CallAdmin
				});
				list.Add(new List<InlineKeyboardButton>());
				list[list.Count - 1].Add(new InlineKeyboardButton()
				{
					Text = "⚙Язык",
					CallbackData = CommandName.Language
				});
			}
			else
			{
				list[list.Count - 1].Add(new InlineKeyboardButton()
				{
					Text = "🛠Find an artist",
					CallbackData = CommandName.FindExecutor
				});
				list[list.Count - 1].Add(new InlineKeyboardButton()
				{
					Text = "📢Looking work",
					CallbackData = CommandName.FindWork
				});

				list.Add(new List<InlineKeyboardButton>());
				list[list.Count - 1].Add(new InlineKeyboardButton()
				{
					Text = "📞Call administrator",
					CallbackData = CommandName.CallAdmin
				});
				list.Add(new List<InlineKeyboardButton>());
				list[list.Count - 1].Add(new InlineKeyboardButton()
				{
					Text = "⚙Language",
					CallbackData = CommandName.Language
				});
			}

			return new InlineKeyboardMarkup(list);
		}

		public static InlineKeyboardMarkup FindAnArtistBackToMenu(User user)
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>
			{
				new List<InlineKeyboardButton>()
			};

			if(user.Language == "RU")
			{
				list[list.Count - 1].Add(new InlineKeyboardButton()
				{
					Text = "🔙Назад",
					CallbackData = CommandName.FindAnArtistBackToMenu
				});
			}
			else
			{
				list[list.Count - 1].Add(new InlineKeyboardButton()
				{
					Text = "🔙Back",
					CallbackData = CommandName.FindAnArtistBackToMenu
				});
			}

			return new InlineKeyboardMarkup(list);
		}

		#region Ответ от админов
		public static InlineKeyboardMarkup AnswerAdmin(User user)
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>
			{
				new List<InlineKeyboardButton>()
			};

			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Ответить",
				CallbackData = CommandName.AnswerAdmin + " " + user.Id
			});

			return new InlineKeyboardMarkup(list);
		}

		public static InlineKeyboardMarkup AnswerAdminInPost(User user, String id)
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>
			{
				new List<InlineKeyboardButton>()
			};

			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Подтвердить",
				CallbackData = CommandName.AcceptPost + " " + user.Id + " " + id
			});
			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "Отменить",
				CallbackData = CommandName.CancelPost + " " + user.Id + " " + id
			});

			return new InlineKeyboardMarkup(list);
		}
		#endregion Ответ от админов

		public static InlineKeyboardMarkup Payment()
		{
			List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>
			{
				new List<InlineKeyboardButton>()
			};

			list[list.Count - 1].Add(InlineKeyboardButton.WithPayment("Оплатить!"));

			list[list.Count - 1].Add(new InlineKeyboardButton()
			{
				Text = "🔙Назад",
				CallbackData = CommandName.FindAnArtistBackToMenu
			});

			return new InlineKeyboardMarkup(list);
		}
	}
}
