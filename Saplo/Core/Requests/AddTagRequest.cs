using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
	[DataContract]
	public class AddTagRequest
	{
		[DataMember(Name = "tag")]
		public string Tag { get; set; }

		[DataMember(Name = "collection_id")]
		public int CollectionID { get; set; }

		[DataMember(Name = "text_id")]
		public int TextID { get; set; }

		[DataMember(Name = "category", EmitDefaultValue = false)]
		public TagCategoryType? Category { get; set; }

		[DataMember(Name = "relevance", EmitDefaultValue = false)]
		public decimal? Relevance { get; set; }

		[DataMember(Name = "ext_text_id", EmitDefaultValue = false)]
		public int? ExternalTextID { get; set; }
	}
}