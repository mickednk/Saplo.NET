using System;
using Saplo.Core;

namespace Saplo.EventArgs
{
	public class CollectionCompletedEventArgs : CustomAsyncCompletedEventArgs<Collection>
	{
		public CollectionCompletedEventArgs(Collection result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}