using System;
using Saplo.Core;

namespace Saplo.EventArgs
{
	public class RelatedTextCompletedEventArgs : CustomAsyncCompletedEventArgs<RelatedText[]>
	{
		public RelatedTextCompletedEventArgs(RelatedText[] result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}