using System;

namespace Saplo.EventArgs
{
	public class SuccessMethodCompletedEventArgs : CustomAsyncCompletedEventArgs<bool>
	{
		public SuccessMethodCompletedEventArgs(bool result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}