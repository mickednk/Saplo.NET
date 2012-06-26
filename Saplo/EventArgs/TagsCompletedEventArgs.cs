using System;
using Saplo.Core;

namespace Saplo.EventArgs
{
	public class TagsCompletedEventArgs : CustomAsyncCompletedEventArgs<Tag[]>
	{
		public TagsCompletedEventArgs(Tag[] result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}