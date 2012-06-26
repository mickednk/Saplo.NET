using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Saplo.Core;
using Saplo.EventArgs;

namespace Saplo
{
	public partial class SaploManager
	{
		#region Delegates

		public delegate void AuthenticateCompletedEventHandler(object sender, AuthenticateCompletedEventArgs e);

		public delegate void GetAccountCompletedEventHandler(object sender, GetAccountCompletedEventArgs e);

		public delegate void InvalidateTokenCompletedEventHandler(object sender, AsyncCompletedEventArgs e);

		#endregion

		private readonly HybridDictionary _userStateToLifetime = new HybridDictionary();

		/// <summary>
		///   Authenticate for access token.
		/// </summary>
		/// <param name="api_key"> api key </param>
		/// <param name="secret_key"> secret key </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void AuthenticateAsync(string api_key, string secret_key, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<string, string, AsyncOperation>(
				(apiKey, secretKey, op) =>
				{
					string accessToken = null;
					Exception ex = null;

					try
					{
						accessToken = Authenticate(apiKey, secretKey);
					}
					catch (Exception e)
					{
						ex = e;
					}

					RemoveOperation(op);
					asyncOp.PostOperationCompleted(state => OnAuthenticateCompleted(state as AuthenticateCompletedEventArgs),
					                               new AuthenticateCompletedEventArgs(accessToken, ex, false, op.UserSuppliedState));
				}).BeginInvoke(api_key, secret_key, asyncOp, null, null);
		}

		/// <summary>
		///   Returns account information for authenticated account.
		/// </summary>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void GetAccountAsync(Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<AsyncOperation>(
				op =>
				{
					Account account = null;
					Exception ex = null;
					try
					{
						account = GetAccount();
					}
					catch (Exception exception)
					{
						ex = exception;
					}

					RemoveOperation(op);
					asyncOp.PostOperationCompleted(state => OnGetAccountCompleted(state as GetAccountCompletedEventArgs),
					                               new GetAccountCompletedEventArgs(account, ex, false, op.UserSuppliedState));
				}).BeginInvoke(asyncOp, null, null);
		}

		/// <summary>
		///   Invalidates token.
		/// </summary>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void InvalidateTokenAsync(Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<AsyncOperation>(
				op =>
				{
					Exception ex = null;
					try
					{
						InvalidateToken();
					}
					catch (Exception exception)
					{
						ex = exception;
					}

					RemoveOperation(op);
					asyncOp.PostOperationCompleted(state => OnInvalidateToken(state as AsyncCompletedEventArgs),
					                               new AsyncCompletedEventArgs(ex, false, op.UserSuppliedState));
				}).BeginInvoke(asyncOp, null, null);
		}

		protected virtual void OnAuthenticateCompleted(AuthenticateCompletedEventArgs e)
		{
			if (AuthenticateCompleted != null)
				AuthenticateCompleted(this, e);
		}

		protected virtual void OnGetAccountCompleted(GetAccountCompletedEventArgs e)
		{
			if (GetAccountCompleted != null)
				GetAccountCompleted(this, e);
		}

		protected virtual void OnInvalidateToken(AsyncCompletedEventArgs e)
		{
			if (InvalidateTokenCompleted != null)
				InvalidateTokenCompleted(this, e);
		}

		private AsyncOperation CreateOperationFromTaskId(Guid taskId)
		{
			var asyncOp = AsyncOperationManager.CreateOperation(taskId);
			lock (_userStateToLifetime.SyncRoot)
			{
				if (_userStateToLifetime.Contains(taskId))
					throw new ArgumentException("The unique ID is not that unique!", "taskId");

				_userStateToLifetime[taskId] = asyncOp;
			}

			return asyncOp;
		}

		private void RemoveOperation(AsyncOperation operation)
		{
			lock (_userStateToLifetime.SyncRoot)
			{
				_userStateToLifetime.Remove(operation.UserSuppliedState);
			}
		}

		private void RemoveOperation(Guid taskId)
		{
			lock (_userStateToLifetime.SyncRoot)
			{
				_userStateToLifetime.Remove(taskId);
			}
		}

		public event AuthenticateCompletedEventHandler AuthenticateCompleted;
		public event GetAccountCompletedEventHandler GetAccountCompleted;
		public event InvalidateTokenCompletedEventHandler InvalidateTokenCompleted;
	}
}