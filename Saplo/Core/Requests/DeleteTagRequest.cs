using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
	public class DeleteTagRequest
	{
		[DataMember(Name = "tag")]
		public string Tag { get; set; }

		[DataMember(Name = "collection_id")]
		public int CollectionID { get; set; }

		[DataMember(Name = "text_id")]
		public int TextID { get; set; }

		[DataMember(Name = "ext_text_id", EmitDefaultValue = false)]
		public int? ExternalTextID { get; set; }
	}
}