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
	internal class AnswerAdmin : ICommand, ISendMessage, IUserDataBase, IMessage, ISplitName
	{
		public System.String Name { set; get; } = CommandName.AnswerAdmin;

		private CallbackQuery _message = null;
		private Database db = null;
		private User user = null;
		private User userTwo = null;
		private Questions questions = null;

		public void Execute(TelegramBotClient botClient, Object message)
		{
			IUserDataBase userDataBase = new AnswerAdmin();
			IMessage iMessage = new AnswerAdmin();
			ISplitName splitName = new AnswerAdmin();

			if (iMessage.SetMessage(message, out _message)) return;

			if (userDataBase.SetDataBase(out db)) return;

			if (userDataBase.SetUser(_message, out user, db)) return;

			Int32 id = splitName.GetNameSplit(Name);
			Name = CommandName.AnswerAdmin;

			if (userDataBase.SetUser(id, out userTwo, db)) return;

			questions = db.LoadRecordById<Questions>("Questions", userTwo.questions);

			ChangeUser();

			SendMessage(botClient);
		}

		private void ChangeUser()
		{
			user.Chain = (Int32)Chain.AnswerAdmin;
			db.UpsertRecord("Users", user.Id, user);
		}

		public async void SendMessage(TelegramBotClient botClient)
		{
			await Task.Run(() => botClient.SendText(_message.From.Id, $"{user.Username} отправьте ответ: \n\n{questions.Question}"));
			await Task.Run(() => botClient.EditMessage(CommandName.Chat, _message.Message.MessageId, $"{user.Username} ответит на сообщение: \n\n{questions.Question}", "46 - AnswerAdmin"));
		}
	}
}
