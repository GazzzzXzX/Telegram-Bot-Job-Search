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
	internal class CallAdmin : AbstractHandler, ISendMessage, ITextMessage, IUserDataBase, IMessage
	{
		Message _message = null;
		User user = null;
		Database db = null;
		String TextMessage = null;

		public override System.Object Handle(System.Int32 request, TelegramBotClient botClient, System.Object message)
		{
			if (request == (Int32)Chain.CallAdmin)
			{
				IMessage iMessage = new CallAdmin();
				IUserDataBase userDataBase = new CallAdmin();
				ITextMessage textMessage = new CallAdmin();

				if (iMessage.SetMessage(message, out _message)) return null;

				if (userDataBase.SetDataBase(out db)) return null;

				if (userDataBase.SetUser(_message, out user, db)) return null;

				TextMessage = textMessage.SetTextMessage(_message, "/ChainCallAdmin", user.Language);

				ChangeUser();

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
			user.Chain = (Int32)Chain.Null;
			db.InsertRecord("Questions", new Questions { user = user, Question = _message.Text });
			var questions = db.LoadRecords<Questions>("Questions");
			user.questions = questions[questions.Count - 1].Id;
		}

		public async void SendMessage(TelegramBotClient botClient)
		{
			await Task.Run(() => botClient.DeleteMessage(_message.From.Id, _message.MessageId, "33 - CallAdmin"));
			await Task.Run(() => botClient.EditMessage(_message.From.Id, user.MessageId, TextMessage, "55 - CallAdmin", user, replyMarkup: InlineButton.FindAnArtistBackToMenu(user)));
			await Task.Run(() => botClient.SendText(CommandName.Chat, _message.Text, replyMarkup: InlineButton.AnswerAdmin(user)));
		}
	}
}
