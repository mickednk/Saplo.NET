using System;
using System.Runtime.Serialization;

namespace Saplo.Core
{
	[DataContract]
	public class Collection : IEquatable<Collection>
	{
		[DataMember(Name = "collection_id")]
		public int CollectionID { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "language")]
		public string Language { get; set; }

		[DataMember(Name = "next_id", IsRequired = false)]
		public int NextID { get; set; }

		[DataMember(Name = "permission", IsRequired = false)]
		public string Permission { get; set; }

		public bool Equals(Collection other)
		{
			return other != null && CollectionID.Equals(other.CollectionID);
		}
	}
}