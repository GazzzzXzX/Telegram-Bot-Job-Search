using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using WorkShop.Bot.Button;
using WorkShop.Bot.ICommand_Interface;
using WorkShop.DataBase;
using User = WorkShop.DataBase.User;

namespace WorkShop.Bot.Command
{
	class Russian : ICommand, ISendMessage, IUserDataBase, IMessage, ITextMessage
	{
		public System.String Name { set; get; } = CommandName.Russian;

		CallbackQuery _message = null;
		String TextMessage = null;
		Database db = null;
		User user = null;

		public void Execute(TelegramBotClient botClient, Object message)
		{
			ICommand command = new Russian();
			IUserDataBase userDataBase = new Russian();
			IMessage iMessage = new Russian();
			ITextMessage textMessage = new Russian();

			if (iMessage.SetMessage(message, out _message)) return;

			if (userDataBase.SetDataBase(out db)) return;

			if (userDataBase.SetUser(_message, out user, db)) return;

			UpdateUser();

			TextMessage = textMessage.SetTextMessage(_message, Name, user.Language);
			if (TextMessage != null)
			{
				SendMessage(botClient);
			}
		}


		private void UpdateUser()
		{
			user.Language = "RU";

			db.UpsertRecord<User>("Users", _message.From.Id, user);
		}

		public async void SendMessage(TelegramBotClient botClient)
		{
			await Task.Run(() => botClient.EditMessage(_message.From.Id, user.MessageId, TextMessage, "53 - Russian", user, replyMarkup: InlineButton.Menu(user)));
		}
	}
}
