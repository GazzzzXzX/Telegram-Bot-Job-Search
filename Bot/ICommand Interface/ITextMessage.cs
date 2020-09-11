using System;
using Microsoft.VisualBasic.FileIO;
using Telegram.Bot.Types;

namespace WorkShop.Bot.ICommand_Interface
{
	interface ITextMessage
	{
		public String SetTextMessage(CallbackQuery callbackQuery, String Name, String lan = null)
		{
			using (TextFieldParser fp = new TextFieldParser($"{AppDomain.CurrentDomain.BaseDirectory}\\language.csv"))
			{
				fp.TextFieldType = FieldType.Delimited;
				fp.SetDelimiters("|");

				while (!fp.EndOfData)
				{
					String[] fields = fp.ReadFields();
					if (Name == "/start")
					{
						if (callbackQuery.From.LanguageCode == "ru")
						{
							return fields[1].Replace("<br>", "\n");
						}
						else
						{
							return fields[2];
						}
					}
					else if (fields[0] == Name)
					{
						if (Name == "/Russian" || Name == "/English")
						{
							return fields[1].Replace("<br>", "\n");
						}

						if (lan == "RU")
						{
							return fields[1].Replace("<br>", "\n");
						}
						else
						{
							return fields[2].Replace("<br>", "\n");
						}
					}
				}
				return null;
			}
		}

		public String SetTextMessage(Message callbackQuery, String Name, String Lan = null)
		{
			using (TextFieldParser fp = new TextFieldParser($"{AppDomain.CurrentDomain.BaseDirectory}\\language.csv"))
			{
				fp.TextFieldType = FieldType.Delimited;
				fp.SetDelimiters("|");

				while (!fp.EndOfData)
				{
					String[] fields = fp.ReadFields();
					if (Name == "/start")
					{
						if (callbackQuery.From.LanguageCode == "ru")
						{
							return fields[1].Replace("<br>", "\n");
						}
						else
						{
							return fields[2].Replace("<br>", "\n");
						}
					}
					else if (fields[0] == Name)
					{
						if (Name == "/Russian" || Name == "/English")
						{
							return fields[1].Replace("<br>", "\n");
						}
						if (Lan == "RU")
						{
							return fields[1].Replace("<br>", "\n");
						}
						else
						{
							return fields[2].Replace("<br>", "\n");
						}
					}
				}
				return null;
			}
		}
	}
}
