using System;
using Saplo.Core;

namespace Saplo.EventArgs
{
	public class TagCompletedEventArgs : CustomAsyncCompletedEventArgs<Tag>
	{
		public TagCompletedEventArgs(Tag result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}