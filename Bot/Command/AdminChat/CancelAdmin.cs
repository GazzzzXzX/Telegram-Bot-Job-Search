using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;
using WorkShop.Bot.Button;
using WorkShop.Bot.ICommand_Interface;
using WorkShop.DataBase;
using User = WorkShop.DataBase.User;

namespace WorkShop.Bot.Command
{
	class CancelAdmin : ICommand, ISendMessage, IUserDataBase, IMessage, ISplitName, ITextMessage
	{
		public System.String Name { set; get; } = CommandName.CancelPost;

		private CallbackQuery _message = null;
		private Database db = null;
		private User user = null;
		private User userTwo = null;
		private Post post = null;
		private AdminPost adminPost = null;
		private String TextMessage = null;

		public void Execute(TelegramBotClient botClient, Object message)
		{
			IUserDataBase userDataBase = new CancelAdmin();
			IMessage iMessage = new CancelAdmin();
			ISplitName splitName = new CancelAdmin();
			ITextMessage textMessage = new CancelAdmin();

			if (iMessage.SetMessage(message, out _message)) return;

			if (userDataBase.SetDataBase(out db)) return;

			if (userDataBase.SetUser(_message, out user, db)) return;

			TextMessage = textMessage.SetTextMessage(_message, "/CancelAdminSendMessage", userTwo.Language);

			Int32 id = splitName.GetNameSplit(Name);
			String idPost = Convert.ToString(Name.Split(" ")[2]);
			Name = CommandName.CancelPost;

			if (userDataBase.SetUser(id, out userTwo, db)) return;

			SetPost(idPost);

			SendMessage(botClient);
		}

		private void SetPost(String id)
		{
			adminPost = db.LoadRecordByIdAdminPost<AdminPost>("AdminPosts", id);
			post = db.LoadRecordById<Post>("Posts", adminPost.PostId);

			adminPost.IsCheck = true;

			db.UpsertRecord<AdminPost>("Posts", adminPost.Id, adminPost);
			db.DeleteRecord<Post>("Posts", post.Id);
		}

		public async void SendMessage(TelegramBotClient botClient)
		{
			await Task.Run(() => botClient.SendText(userTwo.Id, $"{TextMessage}{adminPost.PostText}"));
			await Task.Run(() => botClient.EditMessage(CommandName.Chat, _message.Message.MessageId, $"{user.Username} отменил пост \n\n{adminPost.PostText}", "46 - AnswerAdmin"));
		}
	}
}
