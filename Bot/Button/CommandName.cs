using System;
using System.Collections.Generic;
using System.Text;

namespace WorkShop.Bot.Button
{
	static class CommandName
	{
		public static String Start = "/start";
		public static Int64 Chat = -1001220607003;

		#region Язык
		public static String Russian = "/Russian";
		public static String English = "/English";
		#endregion Язык

		#region Главное меню

		public static String FindExecutor = "/FindExecutor";
		public static String FindWork = "/FindWork";
		public static String CallAdmin = "/CallAdmin";
		public static String Language = "/Language";

		#endregion Главное меню

		#region Ответ админа

		public static String AnswerAdmin = "/AnswerAdmin";
		public static String AcceptPost = "/AcceptPost";
		public static String CancelPost = "/CancelPost";

		#endregion Ответ админа

		public static String FindAnArtistBackToMenu = "/FindAnArtistBackToMenu";
	}
}
