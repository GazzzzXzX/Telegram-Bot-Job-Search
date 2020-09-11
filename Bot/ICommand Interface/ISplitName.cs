using System;
using System.Collections.Generic;
using System.Text;

namespace WorkShop.Bot.ICommand_Interface
{
	internal interface ISplitName
	{
		public Int32 GetNameSplit(String Name) => System.Convert.ToInt32(Name.Split(" ")[1]);
		public Int64 GetNameSplit64(String Name) => System.Convert.ToInt64(Name.Split(" ")[1]);
	}
}
