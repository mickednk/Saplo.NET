using System;
using Saplo.Core;

namespace Saplo.EventArgs
{
	public class ListTextsCompletedEventArgs : CustomAsyncCompletedEventArgs<GroupText[]>
	{
		public ListTextsCompletedEventArgs(GroupText[] result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}