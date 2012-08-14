using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Saplo.Core;
using Saplo.EventArgs;

namespace Saplo.Managers
{
	public partial class CollectionManager
	{
		#region Delegates

		public delegate void CollectionListTextsCompletedEventHandler(object sender, CollectionListTextsCompletedEventArgs e);

		public delegate void CreateCollectionCompletedEventHandler(object sender, CollectionCompletedEventArgs e);

		public delegate void DeleteCompletedEventHandler(object sender, SuccessMethodCompletedEventArgs e);

		public delegate void GetCollectionCompletedEventHandler(object sender, CollectionCompletedEventArgs e);

		public delegate void ListCompletedEventHandler(object sender, CollectionsCompletedEventArgs e);

		public delegate void ResetCompletedEventHandler(object sender, CollectionCompletedEventArgs e);

		public delegate void UpdateCompletedEventHandler(object sender, CollectionCompletedEventArgs e);

		#endregion

		private readonly HybridDictionary _userStateToLifetime = new HybridDictionary();

		/// <summary>
		/// 	Create a new collection.
		/// </summary>
		/// <param name="name"> name of the collection </param>
		/// <param name="language"> language to use in the collection, (only swedish and english are supported) </param>
		/// <param name="description"> short text describing the content of the collection </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void CreateAsync(string name, string language, string description, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<string, string, string, AsyncOperation>(
				(n, lang, desc, operation) =>
				{
					Collection collection = null;
					Exception exception = null;
					try
					{
						collection = Create(n, lang, desc);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					asyncOp.PostOperationCompleted(state => OnCreateCompleted(state as CollectionCompletedEventArgs),
					                               new CollectionCompletedEventArgs(collection, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(name, language, description, asyncOp, null, null);
		}

		/// <summary>
		/// 	Deletes collection from account.
		/// </summary>
		/// <param name="collectionId"> id of collection to remove </param>
		/// <param name="taskId"> unique identifier this operation </param>
		/// <returns> </returns>
		public void DeleteAsync(int collectionId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<int, AsyncOperation>(
				(collId, operation) =>
				{
					bool deleted = false;
					Exception exception = null;
					try
					{
						deleted = Delete(collId);
					}
					catch (Exception ex)
					{
						exception = ex;
					}

					RemoveOperation(operation);
					asyncOp.PostOperationCompleted(state => OnDeleteCompleted(state as SuccessMethodCompletedEventArgs),
					                               new SuccessMethodCompletedEventArgs(deleted, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(collectionId, asyncOp, null, null);
		}

		/// <summary>
		/// 	Get specfic Collection.
		/// </summary>
		/// <param name="collectionId"> ID of collection to fetch </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		public void GetAsync(int collectionId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<int, AsyncOperation>(
				(collId, operation) =>
				{
					Collection collection = null;
					Exception exception = null;
					try
					{
						collection = Get(collId);
					}
					catch (Exception ex)
					{
						exception = ex;
					}

					RemoveOperation(operation);
					asyncOp.PostOperationCompleted(state => OnGetCompleted(state as CollectionCompletedEventArgs),
					                               new CollectionCompletedEventArgs(collection, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(collectionId, asyncOp, null, null);
		}

		/// <summary>
		/// 	Lists all Collection connect to this account.
		/// </summary>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void ListAsync(Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<AsyncOperation>(
				operation =>
				{
					Collection[] collections = null;
					Exception exception = null;
					try
					{
						collections = List();
					}
					catch (Exception ex)
					{
						exception = ex;
					}

					RemoveOperation(operation);
					asyncOp.PostOperationCompleted(state => OnListCompleted(state as CollectionsCompletedEventArgs),
					                               new CollectionsCompletedEventArgs(collections, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(asyncOp, null, null);
		}

		public void ListTextsAsync(int collectionId, int limit, int minTextId, int maxTextId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<int, int, int, int, AsyncOperation>(
				(collId, lmt, minId, maxId, operation) =>
				{
					Text[] texts = null;
					Exception exception = null;
					try
					{
						texts = ListTexts(collId, lmt, minId, maxId);
					}
					catch (Exception ex)
					{
						exception = ex;
					}

					RemoveOperation(operation);
					asyncOp.PostOperationCompleted(state => OnListTextsCompleted(state as CollectionListTextsCompletedEventArgs),
					                               new CollectionListTextsCompletedEventArgs(texts, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(collectionId, limit, minTextId, maxTextId, asyncOp, null, null);
		}

		/// <summary>
		/// 	Resets an collection.
		/// </summary>
		/// <param name="collectionId"> id of collection to reset </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void ResetAsync(int collectionId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<int, AsyncOperation>(
				(collId, operation) =>
				{
					Collection collection = null;
					Exception exception = null;
					try
					{
						collection = Reset(collId);
					}
					catch (Exception ex)
					{
						exception = ex;
					}

					RemoveOperation(operation);
					asyncOp.PostOperationCompleted(state => OnResetCompleted(state as CollectionCompletedEventArgs),
					                               new CollectionCompletedEventArgs(collection, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(collectionId, asyncOp, null, null);
		}

		/// <summary>
		/// 	Updates the information on an collection.
		/// </summary>
		/// <param name="collection"> collection information to update. </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		public void UpdateAsync(Collection collection, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<Collection, AsyncOperation>(
				(coll, operation) =>
				{
					Collection result = null;
					Exception exception = null;
					try
					{
						result = Update(coll);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					asyncOp.PostOperationCompleted(state => OnUpdateCompleted(state as CollectionCompletedEventArgs),
					                               new CollectionCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(collection, asyncOp, null, null);
		}

		protected virtual void OnCreateCompleted(CollectionCompletedEventArgs e)
		{
			if (CreateCompleted != null) CreateCompleted(this, e);
		}

		protected virtual void OnDeleteCompleted(SuccessMethodCompletedEventArgs e)
		{
			if (DeleteCompleted != null) DeleteCompleted(this, e);
		}

		protected virtual void OnGetCompleted(CollectionCompletedEventArgs e)
		{
			if (GetCompleted != null) GetCompleted(this, e);
		}

		protected virtual void OnListCompleted(CollectionsCompletedEventArgs e)
		{
			if (ListCompleted != null) ListCompleted(this, e);
		}

		protected virtual void OnListTextsCompleted(CollectionListTextsCompletedEventArgs e)
		{
			if (CollectionListTextsCompleted != null) CollectionListTextsCompleted(this, e);
		}

		protected virtual void OnResetCompleted(CollectionCompletedEventArgs e)
		{
			if (ResetCompleted != null) ResetCompleted(this, e);
		}

		protected virtual void OnUpdateCompleted(CollectionCompletedEventArgs e)
		{
			if (UpdateCompleted != null) UpdateCompleted(this, e);
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

		public event ListCompletedEventHandler ListCompleted;
		public event ResetCompletedEventHandler ResetCompleted;
		public event DeleteCompletedEventHandler DeleteCompleted;
		public event CreateCollectionCompletedEventHandler CreateCompleted;
		public event GetCollectionCompletedEventHandler GetCompleted;
		public event UpdateCompletedEventHandler UpdateCompleted;
		public event CollectionListTextsCompletedEventHandler CollectionListTextsCompleted;
	}
}