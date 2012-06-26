using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
	[DataContract]
	internal class UpdateTagRequest
	{
		[DataMember(Name = "original_tag")]
		public string OriginalTag { get; set; }

		[DataMember(Name = "collection_id")]
		public int CollectionID { get; set; }

		[DataMember(Name = "text_id")]
		public int TextID { get; set; }

		[DataMember(Name = "updated_tag", EmitDefaultValue = false)]
		public string UpdatedTag { get; set; }

		[DataMember(Name = "updated_category", EmitDefaultValue = false)]
		public TagCategoryType? UpdatedCategory { get; set; }

		[DataMember(Name = "updated_relevance", EmitDefaultValue = false)]
		public decimal? UpdatedRelevance { get; set; }
	}
}