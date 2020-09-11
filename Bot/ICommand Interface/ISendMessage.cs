using Telegram.Bot;

namespace WorkShop.Bot.ICommand_Interface
{
	internal interface ISendMessage
	{
		public void SendMessage(TelegramBotClient botClient);
	}
}
