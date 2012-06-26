using System;

namespace Saplo.EventArgs
{
	public class AuthenticateCompletedEventArgs : CustomAsyncCompletedEventArgs<string>
	{
		public AuthenticateCompletedEventArgs(string result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}