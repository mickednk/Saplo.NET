using System;
using Saplo.Core;

namespace Saplo.EventArgs
{
	public class GroupCompletedEventArgs : CustomAsyncCompletedEventArgs<Group>
	{
		public GroupCompletedEventArgs(Group result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}