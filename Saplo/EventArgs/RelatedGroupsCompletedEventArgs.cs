using System;
using Saplo.Core;

namespace Saplo.EventArgs
{
	public class RelatedGroupsCompletedEventArgs : CustomAsyncCompletedEventArgs<RelatedGroup[]>
	{
		public RelatedGroupsCompletedEventArgs(RelatedGroup[] result, Exception error, bool cancelled, object userState) : base(result, error, cancelled, userState)
		{
		}
	}
}