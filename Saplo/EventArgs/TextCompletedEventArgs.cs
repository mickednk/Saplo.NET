using System;
using Saplo.Core;

namespace Saplo.EventArgs
{
	public class TextCompletedEventArgs : CustomAsyncCompletedEventArgs<Text>
	{
		public TextCompletedEventArgs(Text result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}