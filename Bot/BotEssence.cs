using System;
using System.Collections.Generic;
using Telegram.Bot;
using WorkShop.Bot.Command;
using WorkShop.Bot.ICommand_Interface;
using WorkShop.Bot.Chain_Of_Responsibility;

namespace WorkShop.Bot
{
	class BotEssence
	{
		protected String ApiKey { get; set; }
		//protected DataBase dataBase { get; set; } = Singleton.GetInstance().Context;

		protected TelegramBotClient BotClient { get; set; }

		protected List<ICommand> commands = new List<ICommand>();

		protected Chain_Of_Responsibility.CallAdmin chain = new Chain_Of_Responsibility.CallAdmin();

		public BotEssence(String Api)
		{
			ApiKey = Api;
			BotClient = new TelegramBotClient(ApiKey);

			SetCommand();
			SetChain();
		}

		private void SetCommand()
		{
			commands = new List<ICommand>()
			{ 
				new Start(),			new Russian(),
				new English(),			new FindExecutor(),
				new FindWork(),			new Command.CallAdmin(),
				new Language(),			new FindAnArtistBackToMenu(),
				new AnswerAdmin(),		new AcceptAdmin(),
				new CancelAdmin()
			};
		}

		private void SetChain()
		{
			chain.SetNext(
				new FindExecutorChain()).SetNext(
				new ChainAnswerAdmin()).SetNext(
				new ChainTaskWork()).SetNext(
				new ChainDeadlines()).SetNext(
				new ChainPayment()).SetNext(
				new ChainContacts()).SetNext(
				new ChainPayWork());
		}
	}
}
