using System.Net;
using Saplo.Core;
using Saplo.Core.Requests;
using Saplo.Core.Responses;

namespace Saplo.Managers
{
	/// <summary>
	///   Manager class used for various operation related to groups.
	/// </summary>
	public partial class GroupManager : SectionManagerBase
	{
		public GroupManager(PerformAuthentication authenticateDelegate)
			: base(authenticateDelegate)
		{
		}

		public GroupManager(PerformAuthentication authenticateDelegate, IWebProxy proxy)
			: base(authenticateDelegate, proxy)
		{
		}

		/// <summary>
		///   Adds an text to an group.
		/// </summary>
		/// <param name="groupId"> id of target group </param>
		/// <param name="collectionId"> id of collection containing text </param>
		/// <param name="textId"> id of text to add </param>
		/// <returns> </returns>
		public bool AddText(int groupId, int collectionId, int textId)
		{
			var reqObj = new RequestBase<GroupTextCreateRequest>
			             {
			             	Method = "group.addTexts",
			             	Parameters = new GroupTextCreateRequest
			             	             {
			             	             	CollectionID = collectionId,
			             	             	GroupID = groupId,
			             	             	TextID = textId
			             	             }
			             };

			return CallSaploApi<SuccessResponse>(reqObj).Success;
		}

		/// <summary>
		///   Creates a new group.
		/// </summary>
		/// <param name="name"> name for the group </param>
		/// <param name="language"> language for this group (swedish and english are the only languages supported) </param>
		/// <param name="description"> a short description of this group </param>
		/// <returns> </returns>
		public Group Create(string name, string language, string description)
		{
			var reqObj = new RequestBase<GroupCreateRequest>
			             {
			             	Method = "group.create",
			             	Parameters = new GroupCreateRequest
			             	             {
			             	             	Name = name,
			             	             	Description = description,
			             	             	Language = language
			             	             }
			             };

			return CallSaploApi<Group>(reqObj);
		}

		/// <summary>
		///   Permanently deletes an group.
		/// </summary>
		/// <param name="groupId"> id of group to delete </param>
		/// <returns> </returns>
		public bool Delete(int groupId)
		{
			var reqObj = new RequestBase<GroupRequest>
			             {
			             	Method = "group.create",
			             	Parameters = new GroupRequest
			             	             {
			             	             	GroupID = groupId
			             	             }
			             };

			return CallSaploApi<SuccessResponse>(reqObj).Success;
		}

		/// <summary>
		///   Removes text from group.
		/// </summary>
		/// <param name="groupId"> id of group to remove text from </param>
		/// <param name="collectionId"> id of collection where text exists </param>
		/// <param name="textId"> id of the text to remove </param>
		/// <returns> </returns>
		public bool DeleteText(int groupId, int collectionId, int textId)
		{
			var reqObj = new RequestBase<GroupTextCreateRequest>
			             {
			             	Method = "group.deleteText",
			             	Parameters = new GroupTextCreateRequest
			             	             {
			             	             	CollectionID = collectionId,
			             	             	GroupID = groupId,
			             	             	TextID = textId
			             	             }
			             };

			return CallSaploApi<SuccessResponse>(reqObj).Success;
		}

		/// <summary>
		///   Fetches a group.
		/// </summary>
		/// <param name="groupId"> id of group to fetch </param>
		/// <returns> </returns>
		public Group Get(int groupId)
		{
			var reqObj = new RequestBase<GroupRequest>
			             {
			             	Method = "group.get",
			             	Parameters = new GroupRequest
			             	             {
			             	             	GroupID = groupId
			             	             }
			             };

			return CallSaploApi<Group>(reqObj);
		}

		/// <summary>
		///   Lists all groups that belongs to account
		/// </summary>
		/// <returns> </returns>
		public Group[] List()
		{
			var reqObj = new RequestBase<object>("group.list", new object());
			return CallSaploApi<GroupsResponse>(reqObj).Groups;
		}

		/// <summary>
		///   Lists all texts within an group.
		/// </summary>
		/// <param name="groupId"> id of group </param>
		/// <returns> </returns>
		public GroupText[] ListTexts(int groupId)
		{
			var reqObj = new RequestBase<GroupRequest>
			             {
			             	Method = "group.listTexts",
			             	Parameters = new GroupRequest
			             	             {
			             	             	GroupID = groupId
			             	             }
			             };

			return CallSaploApi<GroupTextsResponse>(reqObj).Texts;
		}

		/// <summary>
		///   Fetches all groups related to the supplied groupId.
		/// </summary>
		/// <param name="groupId"> id of group to find related groups for </param>
		/// <param name="groupScope"> scope to search within, if not specified we use all groups </param>
		/// <param name="wait"> timeout time for query, max 60s </param>
		/// <returns> </returns>
		public RelatedGroup[] RelatedGroups(int groupId, int[] groupScope = null, int? wait = null)
		{
			var reqObj = new RequestBase<GroupRelatedGroupsRequest>
			             {
			             	Method = "group.relatedGroups",
			             	Parameters = new GroupRelatedGroupsRequest
			             	             {
			             	             	GroupID = groupId,
			             	             	GroupScope = groupScope,
			             	             	Wait = wait
			             	             }
			             };

			return CallSaploApi<RelatedGroupsResponse>(reqObj).RelatedGroups;
		}

		/// <summary>
		///   Get all realted texts for this group and collection.
		/// </summary>
		/// <param name="groupId"> id of group </param>
		/// <param name="collectionId"> id of collection </param>
		/// <param name="wait"> the timeout limit, max 60s </param>
		/// <param name="limit"> maximum number of returning results, default is 5 and max is 50 </param>
		/// <returns> </returns>
		public RelatedText[] RelatedText(int groupId, int collectionId, int? wait = null, int? limit = null)
		{
			var reqObj = new RequestBase<GroupRelatedTextsRequest>
			             {
			             	Method = "group.relatedTexts",
			             	Parameters = new GroupRelatedTextsRequest
			             	             {
			             	             	CollectionScope = new[] {collectionId},
			             	             	GroupID = groupId,
			             	             	Limit = limit,
			             	             	Wait = wait
			             	             }
			             };

			return CallSaploApi<RelatedTextsResponse>(reqObj).RelatedTexts;
		}

		/// <summary>
		///   Resets group, removes all texts.
		/// </summary>
		/// <param name="groupId"> id of group </param>
		/// <returns> </returns>
		public bool Reset(int groupId)
		{
			var reqObj = new RequestBase<GroupRequest>
			             {
			             	Method = "group.reset",
			             	Parameters = new GroupRequest
			             	             {
			             	             	GroupID = groupId
			             	             }
			             };

			return CallSaploApi<object>(reqObj) != null;
		}

		/// <summary>
		///   Updates properies of an group. null value for any property will leave property unmodified.
		/// </summary>
		/// <param name="groupId"> id of group to update properties on </param>
		/// <param name="name"> new name of group </param>
		/// <param name="description"> short decribing text for group </param>
		/// <param name="language"> language for group </param>
		/// <returns> </returns>
		public Group Update(int groupId, string name, string description, string language)
		{
			var reqObj = new RequestBase<GroupModifyRequest>
			             {
			             	Method = "group.update",
			             	Parameters = new GroupModifyRequest
			             	             {
			             	             	GroupID = groupId,
			             	             	Name = name,
			             	             	Description = description,
			             	             	Language = language
			             	             }
			             };

			return CallSaploApi<Group>(reqObj);
		}
	}
}