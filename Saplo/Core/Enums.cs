using System.Runtime.Serialization;

namespace Saplo.Core
{
	[DataContract]
	public enum RelationType
	{
		[EnumMember(Value = "automatic")] Automatic,
		[EnumMember(Value = "semantic")] Semantic,
		[EnumMember(Value = "statistic")] Statistic
	}

	public enum TagCategoryType
	{
		[EnumMember(Value = "person")] Person,
		[EnumMember(Value = "organization")] Organization,
		[EnumMember(Value = "location")] Location,
		[EnumMember(Value = "unknown")] Unknown,
		[EnumMember(Value = "url")] URL
	}
}