using System;
using Saplo.Core;

namespace Saplo.EventArgs
{
	public class CollectionListTextsCompletedEventArgs : CustomAsyncCompletedEventArgs<Text[]>
	{
		public CollectionListTextsCompletedEventArgs(Text[] result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}