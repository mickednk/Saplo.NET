using System;
using Saplo.Core;

namespace Saplo.EventArgs
{
	public class GetAccountCompletedEventArgs : CustomAsyncCompletedEventArgs<Account>
	{
		public GetAccountCompletedEventArgs(Account result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}