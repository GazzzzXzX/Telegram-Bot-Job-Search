using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkShop.Bot.Button;
using WorkShop.Bot.ICommand_Interface;
using WorkShop.DataBase;

namespace WorkShop.Bot
{
	class MyBot : BotEssence
	{
		public AutoResetEvent _exitEvent;

		public MyBot(System.String api) : base(api)
		{
			_exitEvent = new AutoResetEvent(false);

			BotClient.OnMessage += OnMessage;
			BotClient.OnCallbackQuery += OnCallbackQuery;
			BotClient.OnUpdate += OnUpdate;

			BotClient.StartReceiving();
			Console.WriteLine(BotClient.GetMeAsync().Result);
		}

		private async void OnUpdate(Object sender, Telegram.Bot.Args.UpdateEventArgs e)
		{
			var db = Singleton.GetInstance().Context;
			if (e.Update.PreCheckoutQuery != null)
			{
				await BotClient.AnswerPreCheckoutQueryAsync(e.Update.PreCheckoutQuery.Id);
				if (e.Update.PreCheckoutQuery.InvoicePayload == "Pay is post")
				{
					User user = db.LoadRecordById<User>("Users", e.Update.PreCheckoutQuery.From.Id);
					Post post = db.LoadRecordById<Post>("Posts", user.PostId);
					post.IsPay = true;
					post.IsPublication = true;

					db.UpsertRecord("Posts", post.Id, post);

					await Task.Run(() => BotClient.SendText(CommandName.Chat, post.PostText));
				}
			}
		}

		~MyBot()
		{
			_exitEvent.Set();

			Console.WriteLine("Destructing bot....");
		}

		private void OnCallbackQuery(Object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
		{
			//--- Обработчик команд.
			if (commands.Any(p => p.Equals(e.CallbackQuery.Data)))
			{
				ICommand command = commands.FirstOrDefault(p => p.Equals(e.CallbackQuery.Data));
				command.Execute(BotClient, e.CallbackQuery);
			}
		}

		private void OnMessage(Object sender, Telegram.Bot.Args.MessageEventArgs e)
		{
			//--- Обработчик команд.
			if(e.Message.Text == CommandName.Start)
			{
				ICommand command = commands.FirstOrDefault(p => p.Equals(e.Message.Text));
				command.Execute(BotClient, e.Message);
			}
			else if(e.Message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Private)
			{
				var db = Singleton.GetInstance().Context;
				var user = db.LoadRecordById<User>("Users", e.Message.From.Id);
				chain.Handle(user.Chain, BotClient, e.Message);
			}
		}
	}
}
