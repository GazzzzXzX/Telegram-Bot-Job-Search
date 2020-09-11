using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using WorkShop.Bot.Button;
using WorkShop.Bot.ICommand_Interface;
using WorkShop.DataBase;
using User = WorkShop.DataBase.User;

namespace WorkShop.Bot.Chain_Of_Responsibility
{
	class ChainContacts : AbstractHandler, ISendMessage, ITextMessage, IUserDataBase, IMessage
	{
		Message _message = null;
		User user = null;
		Database db = null;
		String TextMessage = null;
		Post post = null;

		public override System.Object Handle(System.Int32 request, TelegramBotClient botClient, System.Object message)
		{
			if (request == (Int32)Chain.Contacts)
			{
				IMessage iMessage = new ChainContacts();
				IUserDataBase userDataBase = new ChainContacts();
				ITextMessage textMessage = new ChainContacts();

				if (iMessage.SetMessage(message, out _message)) return null;

				if (userDataBase.SetDataBase(out db)) return null;

				if (userDataBase.SetUser(_message, out user, db)) return null;

				TextMessage = textMessage.SetTextMessage(_message, "/ChainContacts", user.Language);

				ChangeUser();
				SelectPost();

				SendMessage(botClient);

				return null;
			}
			else
			{
				return base.Handle(request, botClient, message);
			}
		}

		private void ChangeUser()
		{
			user.Chain = (Int32)Chain.PayWork;
		}

		private void SelectPost()
		{
			post = db.LoadRecordByIdPost<Post>("Posts", user.Id);
			post.PostText += "\n\n💰 Оплата:\n" + _message.Text;

			db.UpsertRecord("Posts", post.Id, post);

			db.InsertRecord<AdminPost>("AdminPosts", new AdminPost { userId = user.Id, IsCheck = false, PostId = post.Id, PostText = post.PostText });
		}

		public async void SendMessage(TelegramBotClient botClient)
		{
			await Task.Run(() => botClient.DeleteMessage(_message.From.Id, _message.MessageId, "54 - ChainTaskWork"));
			await Task.Run(() => botClient.EditMessage(_message.From.Id, user.MessageId, TextMessage, "55 - CallAdmin", user, replyMarkup: InlineButton.FindAnArtistBackToMenu(user)));
			await Task.Run(() => botClient.SendText(CommandName.Chat, $"Пост:\n\n{post.PostText}", replyMarkup: InlineButton.AnswerAdminInPost(user, post.Id)));
		}
	}
}
