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
	class ChainAnswerAdmin : AbstractHandler, ISendMessage, IUserDataBase, IMessage, ITextMessage
	{
		Message _message = null;
		User user = null;
		Database db = null;
		Questions questions = null;
		private String TextMessage = null;

		public override System.Object Handle(System.Int32 request, TelegramBotClient botClient, System.Object message)
		{
			if (request == (Int32)Chain.AnswerAdmin)
			{
				IMessage iMessage = new ChainAnswerAdmin();
				IUserDataBase userDataBase = new ChainAnswerAdmin();
				ITextMessage textMessage = new ChainAnswerAdmin();

				if (iMessage.SetMessage(message, out _message)) return null;

				if (userDataBase.SetDataBase(out db)) return null;

				if (userDataBase.SetUser(_message, out user, db)) return null;

				ChangeUser();

				TextMessage = textMessage.SetTextMessage(_message, "/AnswerAdminSendMessage", questions.user.Language);

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
			db.UpsertRecord("Users", user.Id, user);

			questions = db.LoadRecordById<Questions>("Questions", user.questions);
			questions.Answer = _message.Text;
			questions.Admin = user;

			db.UpsertRecord("Questions", questions.Id, questions);
		}

		public async void SendMessage(TelegramBotClient botClient)
		{
			await Task.Run(() => botClient.SendText(CommandName.Chat, $"{user.Username} ответил на вопрос: \n{questions.Question}\n\nОтвет: \n{questions.Answer}"));
			await Task.Run(() => botClient.SendText(questions.user.Id, $"Ваш вопрос: \n{questions.Question}{TextMessage}{questions.Answer}"));
		}
	}
}
