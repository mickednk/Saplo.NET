using System.Net;
using Saplo.Core;
using Saplo.Core.Requests;
using Saplo.Core.Responses;
using Saplo.EventArgs;

namespace Saplo.Managers
{
	/// <summary>
	/// Manager class used to manipulate collections.
	/// </summary>
	public partial class CollectionManager : SectionManagerBase
	{
		public CollectionManager(PerformAuthentication authenticationDelegate)
			: this(authenticationDelegate, null)
		{
		}

		public CollectionManager(PerformAuthentication authenticationDelegate, IWebProxy proxy)
			: base(authenticationDelegate, proxy)
		{
		}

		/// <summary>
		/// Create a new collection.
		/// </summary>
		/// <param name="name">name of the collection</param>
		/// <param name="language">language to use in the collection, (only swedish and english are supported)</param>
		/// <param name="description">short text describing the content of the collection</param>
		/// <returns></returns>
		public Collection Create(string name, string language, string description)
		{
			var reqObj = new RequestBase<CollectionCreateRequest>
			             {
			             	Method = "collection.create",
			             	Parameters = new CollectionCreateRequest
			             	             {
			             	             	Name = name,
			             	             	Language = language,
			             	             	Description = description
			             	             }
			             };

			return CallSaploApi<Collection>(reqObj);
		}

		/// <summary>
		///   Deletes collection from account.
		/// </summary>
		/// <param name="collectionId"> id of collection to remove </param>
		/// <returns> </returns>
		public bool Delete(int collectionId)
		{
			var reqObj = new RequestBase<CollectionRequest>
			             {
			             	Method = "collection.delete",
			             	Parameters = new CollectionRequest
			             	             {
			             	             	CollectionID = collectionId
			             	             }
			             };

			return CallSaploApi<SuccessResponse>(reqObj).Success;
		}

		/// <summary>
		///   Get specfic Collection.
		/// </summary>
		/// <param name="collectionId"> ID of collection to fetch </param>
		public Collection Get(int collectionId)
		{
			var reqObj = new RequestBase<CollectionRequest>
			             {
			             	Method = "collection.get",
			             	Parameters = new CollectionRequest
			             	             {
			             	             	CollectionID = collectionId
			             	             }
			             };

			return CallSaploApi<Collection>(reqObj);
		}

		/// <summary>
		///   Lists all Collection connect to this account.
		/// </summary>
		/// <returns> </returns>
		public Collection[] List()
		{
			var reqObj = new RequestBase<object>("collection.list", new object());
			return CallSaploApi<CollectionsResponse>(reqObj).Collections;
		}

		/// <summary>
		///   Resets an collection.
		/// </summary>
		/// <param name="collectionId"> id of collection to reset </param>
		/// <returns> </returns>
		public Collection Reset(int collectionId)
		{
			var reqObj = new RequestBase<CollectionRequest>
			             {
			             	Method = "collection.reset",
			             	Parameters = new CollectionRequest
			             	             {
			             	             	CollectionID = collectionId
			             	             }
			             };

			return CallSaploApi<Collection>(reqObj);
		}

		/// <summary>
		///   Updates the information on an collection.
		/// </summary>
		/// <param name="collection"> collection information to update. </param>
		public Collection Update(Collection collection)
		{
			var reqObj = new RequestBase<CollectionCreateRequest>
			             {
			             	Method = "collection.update",
			             	Parameters = new CollectionCreateRequest
			             	             {
			             	             	Name = collection.Name,
			             	             	Language = collection.Name,
			             	             	Description = collection.Description
			             	             }
			             };

			return CallSaploApi<Collection>(reqObj);
		}
	}
}