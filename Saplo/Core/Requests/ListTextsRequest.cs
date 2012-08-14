using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
	[DataContract]
	internal class ListTextsRequest
	{
		[DataMember(Name = "collection_id")]
		public int CollectionID { get; set; }

		[DataMember(Name = "limit", EmitDefaultValue = false)]
		public int Limit { get; set; }

		[DataMember(Name = "max_text_id", EmitDefaultValue = false)]
		public int MaxTextID { get; set; }

		[DataMember(Name = "min_text_id", EmitDefaultValue = false)]
		public int MinTextID { get; set; }
	}
}