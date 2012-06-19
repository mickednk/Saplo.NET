using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
	[DataContract]
	public class TextCreateRequest
	{
		[DataMember(Name = "collection_id")]
		public int CollectionID { get; set; }

		[DataMember(Name = "text_id", EmitDefaultValue = false)]
		public int? TextID { get; set; }

		[DataMember(Name = "body", EmitDefaultValue = false)]
		public string Body { get; set; }

		[DataMember(Name = "headline", EmitDefaultValue = false)]
		public string Headline { get; set; }

		[DataMember(Name = "publish_date", EmitDefaultValue = false)]
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
			set { PublishDateString = value == null ? null : value.Value.ToString("s"); }
		}

		[DataMember(Name = "url", EmitDefaultValue = false)]
		public string Url { get; set; }

		[DataMember(Name = "authors", EmitDefaultValue = false)]
		public string Authors { get; set; }

		[DataMember(Name = "ext_text_id", EmitDefaultValue = false)]
		public string ExternalTextID { get; set; }
	}
}