using System;
using System.Runtime.Serialization;

namespace Saplo.Core
{
	[DataContract]
	public class Text : IEquatable<Text>
	{
		[DataMember(Name = "collection_id")]
		public int CollectionID { get; set; }

		[DataMember(Name = "text_id")]
		public int TextID { get; set; }

		[DataMember(Name = "ext_text_id")]
		public string ExternalTextID { get; set; }

		[DataMember(Name = "headline")]
		public string Headline { get; set; }

		[DataMember(Name = "url")]
		public string Url { get; set; }

		public bool Equals(Text other)
		{
			return other != null && TextID.Equals(other.TextID);
		}
	}
}