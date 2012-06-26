using System;
using System.ComponentModel;

namespace Saplo.EventArgs
{
	/// <summary>
	///   Base class for Saplo async event argument classes.
	/// </summary>
	/// <typeparam name="T"> result type from the async method </typeparam>
	public class CustomAsyncCompletedEventArgs<T> : AsyncCompletedEventArgs
	{
		private readonly T _result;

		public CustomAsyncCompletedEventArgs(T result, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
		{
			_result = result;
		}

		public virtual T Result
		{
			get
			{
				RaiseExceptionIfNecessary();
				return _result;
			}
		}
	}
}