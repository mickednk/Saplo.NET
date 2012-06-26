using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Saplo.Core;
using Saplo.EventArgs;

namespace Saplo.Managers
{
	public partial class TextManager
	{
		#region Delegates

		public delegate void CreateCompletedEventHandler(object sender, TextCompletedEventArgs e);

		public delegate void DeleteCompletedEventHandler(object sender, SuccessMethodCompletedEventArgs e);

		public delegate void DeleteTagCompletedEventHandler(object sender, SuccessMethodCompletedEventArgs e);

		public delegate void GetCompletedEventHandler(object sender, TextCompletedEventArgs e);

		public delegate void RelatedGroupsCompletedEventHandler(object sender, RelatedGroupsCompletedEventArgs e);

		public delegate void RelatedTextCompletedEventHandler(object sender, RelatedTextCompletedEventArgs e);

		public delegate void TagCompletedEventHandler(object sender, TagCompletedEventArgs e);

		public delegate void TagsCompletedEventHandler(object sender, TagsCompletedEventArgs e);

		public delegate void UpdateCompletedEventHandler(object sender, TextCompletedEventArgs e);

		public delegate void UpdateTagCompletedEventHandler(object sender, TagCompletedEventArgs e);

		#endregion

		private readonly HybridDictionary _userStateToLifetime = new HybridDictionary();

		/// <summary>
		///   Add tag to text entity.
		/// </summary>
		/// <param name="tag"> the tag string </param>
		/// <param name="collectionId"> id of collection </param>
		/// <param name="textId"> id of text entity </param>
		/// <param name="category"> category for tag </param>
		/// <param name="relevance"> tags relevance, a number between 0 and 1 </param>
		/// <param name="taskId"> unqiue identifier for this operation </param>
		/// <returns> </returns>
		public void AddTagAsync(string tag, int collectionId, int textId, TagCategoryType? category, decimal? relevance, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<string, int, int, TagCategoryType?, decimal?, AsyncOperation>(
				(t, collectionid, textid, cat, rel, operation) =>
				{
					Tag result = null;
					Exception exception = null;
					try
					{
						result = AddTag(t, collectionid, textid, cat, rel);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnAddTagCompleted(state as TagCompletedEventArgs),
					                                 new TagCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(tag, collectionId, textId, category, relevance, asyncOp, null, null);
		}

		/// <summary>
		///   Creates a new text entity.
		/// </summary>
		/// <param name="collectionId"> id of collection to put the entity </param>
		/// <param name="body"> the content of entity </param>
		/// <param name="headline"> headline for entity </param>
		/// <param name="url"> url to original "article" </param>
		/// <param name="externalId"> custom external id </param>
		/// <param name="publishdate"> publishdate of original "article" </param>
		/// <param name="taskId"> unqiue identifier for this operation </param>
		/// <returns> </returns>
		public void CreateAsync(int collectionId, string body, string headline, string url, string externalId, DateTime? publishdate, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<int, string, string, string, string, DateTime?, AsyncOperation>(
				(collectionid, bdy, hline, link, extid, startdate, operation) =>
				{
					Text result = null;
					Exception exception = null;
					try
					{
						result = Create(collectionid, bdy, hline, link, extid, startdate);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnCreateCompleted(state as TextCompletedEventArgs),
					                                 new TextCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(collectionId, body, headline, url, externalId, publishdate, asyncOp, null, null);
		}

		/// <summary>
		///   Permanently deletes an text entity.
		/// </summary>
		/// <param name="collectionId"> collection where entity exists </param>
		/// <param name="textId"> id of text entity </param>
		/// <param name="taskId"> unqiue identifier </param>
		/// <returns> </returns>
		public void DeleteAsync(int collectionId, int textId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<int, int, AsyncOperation>(
				(collectionid, textid, operation) =>
				{
					bool result = false;
					Exception exception = null;
					try
					{
						result = Delete(collectionid, textid);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnDeleteCompleted(state as SuccessMethodCompletedEventArgs),
					                                 new SuccessMethodCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(collectionId, textId, asyncOp, null, null);
		}

		/// <summary>
		///   Deletes tag from text entity.
		/// </summary>
		/// <param name="tag"> tag to remove </param>
		/// <param name="collectionId"> id of collection where text exists </param>
		/// <param name="textId"> id of text containing tag </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void DeleteTagAsync(string tag, int collectionId, int textId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<string, int, int, AsyncOperation>(
				(t, collectionid, textid, operation) =>
				{
					bool result = false;
					Exception exception = null;
					try
					{
						result = DeleteTag(t, collectionid, textid);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnDeleteTagCompleted(state as SuccessMethodCompletedEventArgs),
					                                 new SuccessMethodCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(tag, collectionId, textId, asyncOp, null, null);
		}

		/// <summary>
		///   Fetches text entity.
		/// </summary>
		/// <param name="collectionId"> id of collection </param>
		/// <param name="textId"> id of text entity </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void GetAsync(int collectionId, int textId, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<int, int, AsyncOperation>(
				(collectionid, textid, operation) =>
				{
					Text result = null;
					Exception exception = null;
					try
					{
						result = Get(collectionid, textid);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnGetCompleted(state as TextCompletedEventArgs),
					                                 new TextCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(collectionId, textId, asyncOp, null, null);
		}

		/// <summary>
		///   Gets groups related to text entity.
		/// </summary>
		/// <param name="collectionId"> collection where text exists </param>
		/// <param name="textId"> id of text entity </param>
		/// <param name="groupScope"> id of groups to compare against </param>
		/// <param name="wait"> timeout for query, max is 60s. default 5s </param>
		/// <param name="limit"> result item limit, max is 50, default is 10 </param>
		/// <param name="minThreshold"> minimum relevance that resultitem should have, 0,5 == 50%, default is 0 </param>
		/// <param name="maxThreshold"> maximum relevance that resultitem should have, 0,5 == 50%, default is 1 </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void RelatedGroupsAsync(int collectionId, int textId, int[] groupScope, int? wait, int? limit, decimal? minThreshold, decimal? maxThreshold, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<int, int, int[], int?, int?, decimal?, decimal?, AsyncOperation>(
				(collectionid, textid, groupscope, w, l, minthreshold, maxthreshold, operation) =>
				{
					RelatedGroup[] result = null;
					Exception exception = null;
					try
					{
						result = RelatedGroups(collectionid, textid, groupscope, w, l, minthreshold, maxthreshold);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnRelatedGroupsCompleted(state as RelatedGroupsCompletedEventArgs),
					                                 new RelatedGroupsCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(collectionId, textId, groupScope, wait, limit, minThreshold, maxThreshold, asyncOp, null, null);
		}

		/// <summary>
		///   Gets texts realted to supplied textId.
		/// </summary>
		/// <param name="collectionId"> id of collection where text entity exists </param>
		/// <param name="textId"> id of text entity </param>
		/// <param name="relatedBy"> the type of relation to look for. </param>
		/// <param name="collectionScope"> find related text within supplied collections, default is search within contained collection </param>
		/// <param name="wait"> timeout period for query, max is 60s, default is 5s </param>
		/// <param name="limit"> result item limit, max is 50, default is 10 </param>
		/// <param name="minThreshold"> minimum relevance to be included in result </param>
		/// <param name="maxThreshold"> maximum relevance to be included in result </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void RelatedTextsAsync(int collectionId, int textId, RelationType? relatedBy, int[] collectionScope, int? wait, int? limit, decimal? minThreshold, decimal? maxThreshold, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<int, int, RelationType?, int[], int?, int?, decimal?, decimal?, AsyncOperation>(
				(collectionid, textid, relatedby, collectionscope, w, l, minthreshold, maxthreshold, operation) =>
				{
					RelatedText[] result = null;
					Exception exception = null;
					try
					{
						result = RelatedTexts(collectionid, textid, relatedby, collectionscope, w, l, minthreshold, maxthreshold);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnRelatedTextCompleted(state as RelatedTextCompletedEventArgs),
					                                 new RelatedTextCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(collectionId, textId, relatedBy, collectionScope, wait, limit, minThreshold, maxThreshold, asyncOp, null, null);
		}

		/// <summary>
		///   Gets all tags for text entity.
		/// </summary>
		/// <param name="collectionId"> id of collection where text entity exists </param>
		/// <param name="textId"> id of text entity </param>
		/// <param name="wait"> timeout for query, default is 5s, max is 60s </param>
		/// <param name="skip_cat"> if we should skip categorization, might speed up the query </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void TagsAsync(int collectionId, int textId, int? wait, bool? skip_cat, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<int, int, int?, bool?, AsyncOperation>(
				(collectionid, textid, w, skipcat, operation) =>
				{
					Tag[] result = null;
					Exception exception = null;
					try
					{
						result = Tags(collectionid, textid, w, skipcat);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnTagsCompleted(state as TagsCompletedEventArgs),
					                                 new TagsCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(collectionId, textId, wait, skip_cat, asyncOp, null, null);
		}

		/// <summary>
		///   Updates an text entity. send null for any property that shouldn't be changed.
		/// </summary>
		/// <param name="collectionId"> id of collection where text entity exists </param>
		/// <param name="textId"> id of text entity </param>
		/// <param name="headline"> headline for text entity </param>
		/// <param name="body"> text in entity </param>
		/// <param name="url"> url to original "article" </param>
		/// <param name="externalId"> custom external id </param>
		/// <param name="publishdate"> </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void UpdateAsync(int collectionId, int textId, string headline, string body, string url, string externalId, DateTime? publishdate, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<int, int, string, string, string, string, DateTime?, AsyncOperation>(
				(collectionid, textid, hline, bdy, link, extid, pdate, operation) =>
				{
					Text result = null;
					Exception exception = null;
					try
					{
						result = Update(collectionid, textid, hline, bdy, link, extid, pdate);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnUpdateCompleted(state as TextCompletedEventArgs),
					                                 new TextCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(collectionId, textId, headline, body, url, externalId, publishdate, asyncOp, null, null);
		}

		/// <summary>
		///   Updates an text for an text entity. leave any property as null if no modification should be made.
		/// </summary>
		/// <param name="tag"> the tag to update </param>
		/// <param name="collectionId"> id of collection for text entity </param>
		/// <param name="textId"> id of text entity </param>
		/// <param name="newTag"> new name of tag </param>
		/// <param name="newCategory"> new category for tag </param>
		/// <param name="newRelevance"> new relevance </param>
		/// <param name="taskId"> unique identifier for this operation </param>
		/// <returns> </returns>
		public void UpdateTagAsync(string tag, int collectionId, int textId, string newTag, TagCategoryType? newCategory, decimal? newRelevance, Guid taskId)
		{
			var asyncOp = CreateOperationFromTaskId(taskId);

			new Action<string, int, int, string, TagCategoryType?, decimal?, AsyncOperation>(
				(t, collectionid, textid, newtag, newcategory, newrelevance, operation) =>
				{
					Tag result = null;
					Exception exception = null;
					try
					{
						result = UpdateTag(t, collectionid, textid, newtag, newcategory, newrelevance);
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					RemoveOperation(operation);
					operation.PostOperationCompleted(state => OnUpdateTagCompleted(state as TagCompletedEventArgs),
					                                 new TagCompletedEventArgs(result, exception, false, operation.UserSuppliedState));
				}).BeginInvoke(tag, collectionId, textId, newTag, newCategory, newRelevance, asyncOp, null, null);
		}

		protected virtual void OnAddTagCompleted(TagCompletedEventArgs e)
		{
			if (AddTagCompleted != null) AddTagCompleted(this, e);
		}

		protected virtual void OnCreateCompleted(TextCompletedEventArgs e)
		{
			if (CreateCompleted != null) CreateCompleted(this, e);
		}

		protected virtual void OnDeleteCompleted(SuccessMethodCompletedEventArgs e)
		{
			if (DeleteCompleted != null) DeleteCompleted(this, e);
		}

		protected virtual void OnDeleteTagCompleted(SuccessMethodCompletedEventArgs e)
		{
			if (DeleteTagCompleted != null) DeleteTagCompleted(this, e);
		}

		protected virtual void OnGetCompleted(TextCompletedEventArgs e)
		{
			if (GetCompleted != null) GetCompleted(this, e);
		}

		protected virtual void OnRelatedGroupsCompleted(RelatedGroupsCompletedEventArgs e)
		{
			if (RelatedGroupsCompleted != null) RelatedGroupsCompleted(this, e);
		}

		protected virtual void OnRelatedTextCompleted(RelatedTextCompletedEventArgs e)
		{
			if (RelatedTextCompleted != null) RelatedTextCompleted(this, e);
		}

		protected virtual void OnTagsCompleted(TagsCompletedEventArgs e)
		{
			if (TagsCompleted != null) TagsCompleted(this, e);
		}

		protected virtual void OnUpdateCompleted(TextCompletedEventArgs e)
		{
			if (UpdateCompleted != null) UpdateCompleted(this, e);
		}

		protected virtual void OnUpdateTagCompleted(TagCompletedEventArgs e)
		{
			if (UpdateTagCompleted != null) UpdateTagCompleted(this, e);
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

		public event UpdateTagCompletedEventHandler UpdateTagCompleted;

		public event UpdateCompletedEventHandler UpdateCompleted;

		public event TagsCompletedEventHandler TagsCompleted;

		public event RelatedTextCompletedEventHandler RelatedTextCompleted;

		public event RelatedGroupsCompletedEventHandler RelatedGroupsCompleted;

		public event GetCompletedEventHandler GetCompleted;

		public event DeleteTagCompletedEventHandler DeleteTagCompleted;

		public event DeleteCompletedEventHandler DeleteCompleted;

		public event CreateCompletedEventHandler CreateCompleted;

		public event TagCompletedEventHandler AddTagCompleted;
	}
}