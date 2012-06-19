using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Saplo.Core
{
	[DataContract]
	public class Group : IEquatable<Group>
	{
		[DataMember(Name = "group_id")]
		public int GroupID { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "language")]
		public string Language { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "date_created")]
		private string CreatedString { get; set; }

		public DateTime? Created
		{
			get
			{
				DateTime date;
				if (DateTime.TryParse(CreatedString, null, DateTimeStyles.RoundtripKind, out date))
					return date;
				return null;
			}
		}

		[DataMember(Name = "date_updated")]
		private string UpdatedString { get; set; }

		public DateTime? Updated
		{
			get
			{
				DateTime date;
				if (DateTime.TryParse(UpdatedString, null, DateTimeStyles.RoundtripKind, out date))
					return date;
				return null;
			}
		}

		public bool Equals(Group other)
		{
			return other != null && GroupID.Equals(other.GroupID);
		}
	}
}