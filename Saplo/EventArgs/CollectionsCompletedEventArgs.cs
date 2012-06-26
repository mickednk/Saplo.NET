using System;
using Saplo.Core;

namespace Saplo.EventArgs
{
	public class CollectionsCompletedEventArgs : CustomAsyncCompletedEventArgs<Collection[]>
	{
		public CollectionsCompletedEventArgs(Collection[] result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}