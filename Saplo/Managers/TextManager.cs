using System;
using System.Net;
using Saplo.Core;
using Saplo.Core.Requests;
using Saplo.Core.Responses;

namespace Saplo.Managers
{
	/// <summary>
	///   Manager to manage text entities.
	/// </summary>
	public class TextManager : SectionManagerBase
	{
		public TextManager(PerformAuthentication authenticateDelegate)
			: base(authenticateDelegate, null)
		{
		}

		public TextManager(PerformAuthentication authenticateDelegate, IWebProxy proxy)
			: base(authenticateDelegate, proxy)
		{
		}

		/// <summary>
		///   Add tag to text entity.
		/// </summary>
		/// <param name="tag"> the tag string </param>
		/// <param name="collectionId"> id of collection </param>
		/// <param name="textId"> id of text entity </param>
		/// <param name="category"> category for tag </param>
		/// <param name="relevance"> tags relevance, a number between 0 and 1 </param>
		/// <returns> </returns>
		public Tag AddTag(string tag, int collectionId, int textId, TagCategoryType? category = null, decimal? relevance = null)
		{
			var reqObj = new RequestBase<AddTagRequest>
			             {
			             	Method = "text.addTag",
			             	Parameters = new AddTagRequest
			             	             {
			             	             	Category = category,
			             	             	CollectionID = collectionId,
			             	             	ExternalTextID = null,
			             	             	Relevance = relevance,
			             	             	Tag = tag,
			             	             	TextID = textId
			             	             }
			             };

			return CallSaploApi<Tag>(reqObj);
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
		/// <returns> </returns>
		public Text Create(int collectionId, string body, string headline = null, string url = null, string externalId = null, DateTime? publishdate = null)
		{
			var reqObj = new RequestBase<TextCreateRequest>
			             {
			             	Method = "text.create",
			             	Parameters = new TextCreateRequest
			             	             {
			             	             	CollectionID = collectionId,
			             	             	Body = body,
			             	             	Headline = headline,
			             	             	ExternalTextID = externalId,
			             	             	PublishDate = publishdate,
			             	             	Url = url
			             	             }
			             };

			return CallSaploApi<Text>(reqObj);
		}

		/// <summary>
		///   Permanently deletes an text entity.
		/// </summary>
		/// <param name="collectionId"> collection where entity exists </param>
		/// <param name="textId"> id of text entity </param>
		/// <returns> </returns>
		public bool Delete(int collectionId, int textId)
		{
			var reqObj = new RequestBase<TextRequest>
			             {
			             	Method = "text.delete",
			             	Parameters = new TextRequest
			             	             {
			             	             	CollectionID = collectionId,
			             	             	TextID = textId
			             	             }
			             };

			return CallSaploApi<SuccessResponse>(reqObj).Success;
		}

		/// <summary>
		///   Deletes tag from text entity.
		/// </summary>
		/// <param name="tag"> tag to remove </param>
		/// <param name="collectionId"> id of collection where text exists </param>
		/// <param name="textId"> id of text containing tag </param>
		/// <returns> </returns>
		public bool DeleteTag(string tag, int collectionId, int textId)
		{
			var reqObj = new RequestBase<DeleteTagRequest>
			             {
			             	Method = "deleteTag",
			             	Parameters = new DeleteTagRequest
			             	             {
			             	             	Tag = tag,
			             	             	CollectionID = collectionId,
			             	             	TextID = textId
			             	             }
			             };
			return CallSaploApi<SuccessResponse>(reqObj).Success;
		}

		/// <summary>
		///   Fetches text entity.
		/// </summary>
		/// <param name="collectionId"> id of collection </param>
		/// <param name="textId"> id of text entity </param>
		/// <returns> </returns>
		public Text Get(int collectionId, int textId)
		{
			var reqObj = new RequestBase<TextRequest>
			             {
			             	Method = "text.get",
			             	Parameters = new TextRequest
			             	             {
			             	             	CollectionID = collectionId,
			             	             	TextID = textId
			             	             }
			             };

			return CallSaploApi<Text>(reqObj);
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
		/// <returns> </returns>
		public RelatedGroup[] RelatedGroups(int collectionId, int textId, int[] groupScope = null, int? wait = null, int? limit = null, decimal? minThreshold = null, decimal? maxThreshold = null)
		{
			var reqObj = new RequestBase<TextRelatedGroupsRequest>
			             {
			             	Method = "text.relatedGroups",
			             	Parameters = new TextRelatedGroupsRequest
			             	             {
			             	             	CollectionID = collectionId,
			             	             	TextID = textId,
			             	             	GroupScope = groupScope,
			             	             	Wait = wait,
			             	             	Limit = limit,
			             	             	MinThreshold = minThreshold,
			             	             	MaxThreshold = maxThreshold
			             	             }
			             };

			return CallSaploApi<RelatedGroupsResponse>(reqObj).RelatedGroups;
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
		/// <returns> </returns>
		public RelatedText[] RelatedTexts(int collectionId, int textId, RelationType? relatedBy = null, int[] collectionScope = null, int? wait = null, int? limit = null, decimal? minThreshold = null, decimal? maxThreshold = null)
		{
			var reqObj = new RequestBase<TextRelatedTextsRequest>
			             {
			             	Method = "text.relatedTexts",
			             	Parameters = new TextRelatedTextsRequest
			             	             {
			             	             	CollectionID = collectionId,
			             	             	TextID = textId,
			             	             	RelatedBy = relatedBy,
			             	             	CollectionScope = collectionScope,
			             	             	Wait = wait,
			             	             	Limit = limit,
			             	             	MinThreshold = minThreshold,
			             	             	MaxThreshold = maxThreshold,
			             	             }
			             };

			return CallSaploApi<RelatedTextsResponse>(reqObj).RelatedTexts;
		}

		/// <summary>
		///   Gets all tags for text entity.
		/// </summary>
		/// <param name="collectionId"> id of collection where text entity exists </param>
		/// <param name="textId"> id of text entity </param>
		/// <param name="wait"> timeout for query, default is 5s, max is 60s </param>
		/// <param name="skip_cat"> if we should skip categorization, might speed up the query </param>
		/// <returns> </returns>
		public Tag[] Tags(int collectionId, int textId, int? wait = null, bool? skip_cat = null)
		{
			var reqObj = new RequestBase<TagRequest>
			             {
			             	Method = "text.tags",
			             	Parameters = new TagRequest
			             	             {
			             	             	CollectionID = collectionId,
			             	             	TextID = textId,
			             	             	Wait = wait,
			             	             	SkipCategorization = skip_cat
			             	             }
			             };

			return CallSaploApi<TagsResponse>(reqObj).Tags;
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
		/// <returns> </returns>
		public Text Update(int collectionId, int textId, string headline = null, string body = null, string url = null, string externalId = null, DateTime? publishdate = null)
		{
			var reqObj = new RequestBase<TextCreateRequest>
			             {
			             	Method = "text.update",
			             	Parameters = new TextCreateRequest
			             	             {
			             	             	CollectionID = collectionId,
			             	             	TextID = textId,
			             	             	Body = body,
			             	             	Headline = headline,
			             	             	ExternalTextID = externalId,
			             	             	PublishDate = publishdate,
			             	             	Url = url
			             	             }
			             };

			return CallSaploApi<Text>(reqObj);
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
		/// <returns> </returns>
		public Tag UpdateTag(string tag, int collectionId, int textId, string newTag = null, TagCategoryType? newCategory = null, decimal? newRelevance = null)
		{
			var reqObj = new RequestBase<UpdateTagRequest>
			             {
			             	Method = "text.updateTag",
			             	Parameters = new UpdateTagRequest
			             	             {
			             	             	CollectionID = collectionId,
			             	             	OriginalTag = tag,
			             	             	TextID = textId,
			             	             	UpdatedCategory = newCategory,
			             	             	UpdatedTag = newTag,
			             	             	UpdatedRelevance = newRelevance
			             	             }
			             };

			return CallSaploApi<Tag>(reqObj);
		}
	}
}