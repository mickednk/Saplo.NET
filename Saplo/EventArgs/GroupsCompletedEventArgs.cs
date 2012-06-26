using System;
using Saplo.Core;

namespace Saplo.EventArgs
{
	public class GroupsCompletedEventArgs : CustomAsyncCompletedEventArgs<Group[]>
	{
		public GroupsCompletedEventArgs(Group[] result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}