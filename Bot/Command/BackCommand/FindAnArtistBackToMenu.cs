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
	class FindAnArtistBackToMenu : ICommand, ISendMessage, IUserDataBase, IMessage, ITextMessage
	{
		public System.String Name { set; get; } = CommandName.FindAnArtistBackToMenu;

		CallbackQuery _message = null;
		String TextMessage = null;
		Database db = null;
		User user = null;
		Post post = null;

		public void Execute(TelegramBotClient botClient, Object message)
		{
			ICommand command = new FindAnArtistBackToMenu();
			IUserDataBase userDataBase = new FindAnArtistBackToMenu();
			IMessage iMessage = new FindAnArtistBackToMenu();
			ITextMessage textMessage = new FindAnArtistBackToMenu();

			if (iMessage.SetMessage(message, out _message)) return;

			if (userDataBase.SetDataBase(out db)) return;

			if (userDataBase.SetUser(_message, out user, db)) return;

			ChangeUser();
			ChangePost();

			TextMessage = textMessage.SetTextMessage(_message, Name, user.Language);
			if (TextMessage != null)
			{
				SendMessage(botClient);
			}
		}

		private void ChangePost()
		{
			post = db.LoadRecordByIdPost<Post>("Posts", user.Id);
			if (post != null)
			{
				db.DeleteRecord<Post>("Posts", post.Id);
			}
			
		}

		private void ChangeUser()
		{
			user.Chain = (Int32)Chain.Null;
			db.UpsertRecord<User>("Users", _message.From.Id, user);
		}

		public async void SendMessage(TelegramBotClient botClient)
		{
			await Task.Run(() => botClient.EditMessage(_message.From.Id, user.MessageId, TextMessage, "50 - FindAnArtistBackToMenu", user, replyMarkup: InlineButton.Menu(user)));
		}
	}
}
