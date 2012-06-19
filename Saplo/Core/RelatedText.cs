using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Saplo.Core
{
	[DataContract]
	public class RelatedText : Text, IComparable<RelatedText>
	{
		[DataMember(Name = "relevance")]
		public decimal Relevance { get; private set; }

		[DataMember(Name = "publish_date")]
		private string PublishDateString { get; set; }

		public DateTime? PublishDate
		{
			get
			{
				DateTime date;
				if (DateTime.TryParse(PublishDateString, null, DateTimeStyles.RoundtripKind, out date))
					return date;
				return null;
			}
		}

		public int CompareTo(RelatedText other)
		{
			//sort using relevance.
			return Relevance.CompareTo(other.Relevance);
		}
	}
}