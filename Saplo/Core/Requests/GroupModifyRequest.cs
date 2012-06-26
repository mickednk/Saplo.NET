using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
	[DataContract]
	internal class GroupModifyRequest
	{
		[DataMember(Name = "group_id", IsRequired = true)]
		public int? GroupID { get; set; }

		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		[DataMember(Name = "language", EmitDefaultValue = false)]
		public string Language { get; set; }

		[DataMember(Name = "description", EmitDefaultValue = false)]
		public string Description { get; set; }
	}
}