using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Saplo.Core;
using Saplo.EventArgs;

namespace Saplo.Managers
{
	public partial class GroupManager
	{
		#region Delegates

		public delegate void AddTextCompletedEventHandler(object sender, SuccessMethodCompletedEventArgs e);

		public delegate void CreateCompletedEventHandler(object sender, GroupCompletedEventArgs e);

		public delegate void DeleteCompletedEventHandler(object sender, SuccessMethodCompletedEventArgs e);

		public delegate void DeleteTextCompletedEventHandler(object sender, SuccessMethodCompletedEventArgs e);

		public delegate void GetCompletedEventHandler(object sender, GroupCompletedEventArgs e);

		public delegate void ListCompletedEventHandler(object sender, GroupsCompletedEventArgs e);

		public delegate void ListTextsCompletedEventHandler(object sender, ListTextsCompletedEventArgs e);

		public delegate void RelatedGroupsCompletedEventHandler(object sender, RelatedGroupsCompletedEventArgs e);

		public delegate void RelatedTextCompletedEventHandler(object sender, RelatedTextCompletedEventArgs e);

		public delegate void ResetCompleteEventHandler(object sender, SuccessMethodCompletedEventArgs e);

		public delegate void UpdateCompletedEventHandler(object sender, GroupCompletedEventArgs e);

		#endregion

		private readonly HybridDictionary _userStateToLifetime = new HybridDictionary();

		/// <summary>
		///   Adds an text to an group.
		/// </summary>
		/// <param name="groupId"> id of target group </param>
		/// <param name="collectionId"> id of collection containing text </param>
		/// <param name="textId"> id of text to add </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void AddTextAsync(int groupId, int collectionId, int textId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<int, int, int, AsyncOperation>(
				(groupid, collectionid, textid, operation) =>
				{
					bool result = false;
					Exception exception = null;
					try
					{
						result = AddText(groupid, collectionid, textid);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnAddTextCompleted(state as SuccessMethodCompletedEventArgs),
					                                 new SuccessMethodCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(groupId, collectionId, textId, asyncOp, null, null);
		}

		/// <summary>
		///   Creates a new group.
		/// </summary>
		/// <param name="name"> name for the group </param>
		/// <param name="language"> language for this group (swedish and english are the only languages supported) </param>
		/// <param name="description"> a short description of this group </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void Create(string name, string language, string description, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<string, string, string, AsyncOperation>(
				(n, lang, desc, operation) =>
				{
					Group result = null;
					Exception exception = null;
					try
					{
						result = Create(n, lang, desc);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnCreateCompleted(state as GroupCompletedEventArgs),
					                                 new GroupCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(name, language, description, asyncOp, null, null);
		}

		/// <summary>
		///   Permanently deletes an group.
		/// </summary>
		/// <param name="groupId"> id of group to delete </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void DeleteAsync(int groupId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<int, AsyncOperation>(
				(groupid, operation) =>
				{
					bool result = false;
					Exception exception = null;
					try
					{
						result = Delete(groupid);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnDeleteCompleted(state as SuccessMethodCompletedEventArgs),
					                                 new SuccessMethodCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(groupId, asyncOp, null, null);
		}

		/// <summary>
		///   Removes text from group.
		/// </summary>
		/// <param name="groupId"> id of group to remove text from </param>
		/// <param name="collectionId"> id of collection where text exists </param>
		/// <param name="textId"> id of the text to remove </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void DeleteTextAsync(int groupId, int collectionId, int textId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<int, int, int, AsyncOperation>(
				(groupid, collectionid, textid, operation) =>
				{
					bool result = false;
					Exception exception = null;
					try
					{
						result = DeleteText(groupid, collectionid, textid);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnDeleteTextCompleted(state as SuccessMethodCompletedEventArgs),
					                                 new SuccessMethodCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(groupId, collectionId, textId, asyncOp, null, null);
		}

		/// <summary>
		///   Fetches a group.
		/// </summary>
		/// <param name="groupId"> id of group to fetch </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void GetAsync(int groupId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<int, AsyncOperation>(
				(groupid, operation) =>
				{
					Group result = null;
					Exception exception = null;
					try
					{
						result = Get(groupid);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnGetCompleted(state as GroupCompletedEventArgs),
					                                 new GroupCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(groupId, asyncOp, null, null);
		}

		/// <summary>
		///   Lists all groups that belongs to account
		/// </summary>
		/// <param name="taskId"> unqiue identifier for this operation </param>
		/// <returns> </returns>
		public void ListAsync(Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<AsyncOperation>(
				(operation) =>
				{
					Group[] result = null;
					Exception exception = null;
					try
					{
						result = List();
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnListCompleted(state as GroupsCompletedEventArgs),
					                                 new GroupsCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(asyncOp, null, null);
		}

		/// <summary>
		///   Lists all texts within an group.
		/// </summary>
		/// <param name="groupId"> id of group </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void ListTextsAsync(int groupId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<int, AsyncOperation>(
				(groupid, operation) =>
				{
					GroupText[] result = null;
					Exception exception = null;
					try
					{
						result = ListTexts(groupid);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnListTextsCompleted(state as ListTextsCompletedEventArgs),
					                                 new ListTextsCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(groupId, asyncOp, null, null);
		}

		/// <summary>
		///   Fetches all groups related to the supplied groupId.
		/// </summary>
		/// <param name="groupId"> id of group to find related groups for </param>
		/// <param name="groupScope"> scope to search within, if not specified we use all groups </param>
		/// <param name="wait"> timeout time for query, max 60s </param>
		/// <param name="taskId"> unqiue identifier for this operation </param>
		/// <returns> </returns>
		public void RelatedGroupsAsync(int groupId, int[] groupScope, int? wait, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<int, int[], int?, AsyncOperation>(
				(groupid, groupscope, w, operation) =>
				{
					RelatedGroup[] result = null;
					Exception exception = null;
					try
					{
						result = RelatedGroups(groupid, groupscope, w);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnRelatedGroupsCompleted(state as RelatedGroupsCompletedEventArgs),
					                                 new RelatedGroupsCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(groupId, groupScope, wait, asyncOp, null, null);
		}

		/// <summary>
		///   Get all realted texts for this group and collection.
		/// </summary>
		/// <param name="groupId"> id of group </param>
		/// <param name="collectionId"> id of collection </param>
		/// <param name="wait"> the timeout limit, max 60s </param>
		/// <param name="limit"> maximum number of returning results, default is 5 and max is 50 </param>
		/// <param name="taskId"> unqiue identifier for this operation </param>
		/// <returns> </returns>
		public void RelatedTextAsync(int groupId, int collectionId, int? wait, int? limit, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<int, int, int?, int?, AsyncOperation>(
				(groupid, collectionid, w, l, operation) =>
				{
					RelatedText[] result = null;
					Exception exception = null;
					try
					{
						result = RelatedText(groupid, collectionid, w, l);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnRelatedTextCompleted(state as RelatedTextCompletedEventArgs),
					                                 new RelatedTextCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(groupId, collectionId, wait, limit, asyncOp, null, null);
		}

		/// <summary>
		///   Resets group, removes all texts.
		/// </summary>
		/// <param name="groupId"> id of group </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void ResetAsync(int groupId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<int, AsyncOperation>(
				(groupid, operation) =>
				{
					bool result = false;
					Exception exception = null;
					try
					{
						result = Reset(groupid);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnResetCompleted(state as SuccessMethodCompletedEventArgs),
					                                 new SuccessMethodCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(groupId, asyncOp, null, null);
		}

		/// <summary>
		///   Updates properies of an group. null value for any property will leave property unmodified.
		/// </summary>
		/// <param name="groupId"> id of group to update properties on </param>
		/// <param name="name"> new name of group </param>
		/// <param name="description"> short decribing text for group </param>
		/// <param name="language"> language for group </param>
		/// <param name="taskId"> unqiue identifier for this operation </param>
		/// <returns> </returns>
		public void UpdateAsync(int groupId, string name, string description, string language, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);
			new Action<int, string, string, string, AsyncOperation>(
				(groupid, n, desc, lang, operation) =>
				{
					Group result = null;
					Exception exception = null;
					try
					{
						result = Update(groupid, n, desc, lang);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnUpdateCompleted(state as GroupCompletedEventArgs),
					                                 new GroupCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(groupId, name, description, language, asyncOp, null, null);
		}

		protected virtual void OnAddTextCompleted(SuccessMethodCompletedEventArgs e)
		{
			if (AddTextCompleted != null) AddTextCompleted(this, e);
		}

		protected virtual void OnCreateCompleted(GroupCompletedEventArgs e)
		{
			if (CreateCompleted != null) CreateCompleted(this, e);
		}

		protected virtual void OnDeleteCompleted(SuccessMethodCompletedEventArgs e)
		{
			if (DeleteCompleted != null) DeleteCompleted(this, e);
		}

		protected virtual void OnDeleteTextCompleted(SuccessMethodCompletedEventArgs e)
		{
			if (DeleteTextCompleted != null) DeleteTextCompleted(this, e);
		}

		protected virtual void OnGetCompleted(GroupCompletedEventArgs e)
		{
			if (GetCompleted != null) GetCompleted(this, e);
		}

		protected virtual void OnListCompleted(GroupsCompletedEventArgs e)
		{
			if (ListCompleted != null) ListCompleted(this, e);
		}

		protected virtual void OnListTextsCompleted(ListTextsCompletedEventArgs e)
		{
			if (ListTextsCompleted != null) ListTextsCompleted(this, e);
		}

		protected virtual void OnRelatedGroupsCompleted(RelatedGroupsCompletedEventArgs e)
		{
			if (RelatedGroupsCompleted != null) RelatedGroupsCompleted(this, e);
		}

		protected virtual void OnRelatedTextCompleted(RelatedTextCompletedEventArgs e)
		{
			if (RelatedTextCompleted != null) RelatedTextCompleted(this, e);
		}

		protected virtual void OnResetCompleted(SuccessMethodCompletedEventArgs e)
		{
			if (ResetCompleted != null) ResetCompleted(this, e);
		}

		protected virtual void OnUpdateCompleted(GroupCompletedEventArgs e)
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

		public event UpdateCompletedEventHandler UpdateCompleted;

		public event ResetCompleteEventHandler ResetCompleted;

		public event RelatedTextCompletedEventHandler RelatedTextCompleted;

		public event RelatedGroupsCompletedEventHandler RelatedGroupsCompleted;

		public event ListTextsCompletedEventHandler ListTextsCompleted;

		public event ListCompletedEventHandler ListCompleted;

		public event GetCompletedEventHandler GetCompleted;

		public event DeleteTextCompletedEventHandler DeleteTextCompleted;

		public event DeleteCompletedEventHandler DeleteCompleted;

		public event CreateCompletedEventHandler CreateCompleted;

		public event AddTextCompletedEventHandler AddTextCompleted;
	}
}