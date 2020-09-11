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
	class AcceptAdmin : ICommand, ISendMessage, IUserDataBase, IMessage, ISplitName, ITextMessage
	{
		public System.String Name { set; get; } = CommandName.AcceptPost;

		private CallbackQuery _message = null;
		private Database db = null;
		private User user = null;
		private User userTwo = null;
		private Post post = null;
		private AdminPost adminPost = null;
		private String TextMessage = null;

		public void Execute(TelegramBotClient botClient, Object message)
		{
			IUserDataBase userDataBase = new AcceptAdmin();
			IMessage iMessage = new AcceptAdmin();
			ISplitName splitName = new AcceptAdmin();
			ITextMessage textMessage = new AcceptAdmin();

			if (iMessage.SetMessage(message, out _message)) return;

			if (userDataBase.SetDataBase(out db)) return;

			if (userDataBase.SetUser(_message, out user, db)) return;

			TextMessage = textMessage.SetTextMessage(_message, "/AcceptAdminSendMessage", userTwo.Language);

			Int32 id = splitName.GetNameSplit(Name);
			String idPost = Convert.ToString(Name.Split(" ")[2]);
			Name = CommandName.AcceptPost;

			if (userDataBase.SetUser(id, out userTwo, db)) return;

			SetPost(idPost);

			SendMessage(botClient);
		}

		private void SetPost(String id)
		{
			adminPost = db.LoadRecordByIdAdminPost<AdminPost>("AdminPosts", id);
			post = db.LoadRecordById<Post>("Posts", adminPost.PostId);

			adminPost.IsCheck = true;
			post.IsCheck = true;
			userTwo.PostId = post.Id;

			db.UpsertRecord<User>("Users", userTwo.Id, userTwo);
			db.UpsertRecord<Post>("Posts", post.Id, post);
			db.UpsertRecord<AdminPost>("Posts", adminPost.Id, adminPost);
		}

		public async void SendMessage(TelegramBotClient botClient)
		{
			Message mes = await botClient.SendInvoiceAsync(
				chatId: _message.From.Id,
				title: "Оплата за пост",
				description: $"{TextMessage}{adminPost.PostText}",
				payload: "Pay is post",
				providerToken: "632593626:TEST:i56982357197",
				startParameter: "HEllo",
				currency: "UAH",
				prices: new[] { new LabeledPrice("price", 5000), },
				replyMarkup: InlineButton.Payment()
				);
			await Task.Run(() => botClient.EditMessage(CommandName.Chat, _message.Message.MessageId, $"{user.Username} принял пост \n\n{adminPost.PostText}", "46 - AnswerAdmin"));
		}
	}
}
