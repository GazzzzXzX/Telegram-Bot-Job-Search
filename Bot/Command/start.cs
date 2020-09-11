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
	internal class Start : ICommand, ISendMessage, IUserDataBase, IMessage, ITextMessage
	{
		public System.String Name { set; get; } = CommandName.Start;

		private Message _message = null;
		private String TextMessage = null;
		private Database db = null;
		private User user = null;

		public void Execute(TelegramBotClient botClient, Object message)
		{
			ICommand command = new Start();
			IUserDataBase userDataBase = new Start();
			IMessage iMessage = new Start();
			ITextMessage textMessage = new Start();

			if (iMessage.SetMessage(message, out _message)) return;

			if (userDataBase.SetDataBase(out db)) return;

			userDataBase.SetUser(_message, out user, db);
			if (user == null)
			{
				TextMessage = textMessage.SetTextMessage(_message, Name);
			}
			else
			{
				TextMessage = textMessage.SetTextMessage(_message, "/FindAnArtistBackToMenu", user.Language);
			}

			Server();

			if (TextMessage != null)
			{
				SendMessage(botClient);
			}
		}

		private void Server()
		{
			if (user == null)
			{
				db.InsertRecord("Users", new User { Id = _message.From.Id, Username = _message.From.Username != null ? "@" + _message.From.Username : null, Language = null });
			}
		}

		public async void SendMessage(TelegramBotClient botClient)
		{
			await botClient.DeleteMessageAsync(_message.From.Id, _message.MessageId);
			if (user == null)
			{
				user = db.LoadRecordById<User>("Users", _message.From.Id);
				await Task.Run(() => botClient.SendText(_message.From.Id, TextMessage, user, replyMarkup: InlineButton.Start()));
			}
			else
			{
				user = db.LoadRecordById<User>("Users", _message.From.Id);
				await Task.Run(() => botClient.SendText(_message.From.Id, TextMessage, user, replyMarkup: InlineButton.Menu(user)));
			}
		}
	}
}
